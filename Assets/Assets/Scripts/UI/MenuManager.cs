using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using XInputDotNetPure;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [Header("Spash screens")]
    public Image _tSplash;
    public Image _tSplashFade;

    private float _fCurrentTimer = 0.0f;
    private float _fFadeSpeed = 1.0f;
    private float _fWaitTime = 5.0f;

    [Header("Player Selection")]
    public List<Text> _tPlayerInfo = new List<Text>();
    private bool[] _bConnected = new bool[4];
    private bool[] _bValidated = new bool[4];
    private bool[] _bConfirmed = new bool[4];

    private bool _bSelectionFinalize = false;
    private float _fSelectionTimer = 10f;

    private GamePadState[] _tPadState = new GamePadState[4];

    [Header("Menus Root")]
    public GameObject _tSplashRoot;
    public GameObject _tMainMenuRoot;
    public GameObject _tPlayerSelectionRoot;
    public GameObject _tCreditsRoot;
    public GameObject _tLoadingRoot;

    public delegate void BackCallback();
    BackCallback _PreviousMenu = null;

    void Awake()
    {
        Instance = this;
        for( int i = 0; i < 4; i++ )
        {
            _bConnected[i] = false;
            _bValidated[i] = false;
            _bConfirmed[i] = false;
        }

        DisableAll();
        _tSplashRoot.SetActive( true );
    }

    void Update()
    {
        if( Input.GetKeyDown( KeyCode.Escape ) ) {
            Back();
        }

        if( _tSplashRoot.activeSelf )
        {
            _fCurrentTimer += Time.deltaTime;

            float fFade = Mathf.Clamp01( Mathf.Sin( _fCurrentTimer * Mathf.PI / _fWaitTime ) * _fFadeSpeed );

            _tSplashFade.color = new Color(0, 0, 0, fFade);

            if (_fCurrentTimer >= 1.0f || Input.anyKeyDown)
            {
                _tSplashRoot.SetActive( false );
                _tMainMenuRoot.SetActive( true );
            }
        }
        else if( _tPlayerSelectionRoot.activeSelf )
        {

            if( _bSelectionFinalize )
            {
                _fSelectionTimer -= Time.deltaTime;
                if( _fSelectionTimer <= 0f ) {
                    LoadLevel( "MapScene" );
                }
                return;
            } 

            int iConfirmed = 0;
            int iValidated = 0;
            int iPlayers = 0;

            for( int i = 0; i < 4; i++ )
            {
                GamePadState tPrevState = _tPadState[i];
                _tPadState[i] = GamePad.GetState( (PlayerIndex)i );
#if UNITY_EDITOR
                if( !_tPadState[i].IsConnected && GameSettings._iNbPlayers > i )
                {
                    _tPadState[i] = GamePad.GetState( PlayerIndex.One );
                }
#endif

                if( _tPadState[i].IsConnected )
                {
                    iPlayers++;
                    _bConnected[i] = true;
                    if( _bValidated[i] )
                    {
                        iValidated++;
                        if( _bConfirmed[i] )
                        {
                            iConfirmed++;
                        }
                        else if( Joystick.GetButtonDown( XInputKey.A, _tPadState[i], tPrevState ) )
                        {
                            _bConfirmed[i] = true;
                            _tPlayerInfo[i].text = "READY !";
                        }
                    }
                    else if( Joystick.GetButtonDown( XInputKey.A, _tPadState[i], tPrevState ) )
                    {
                        _bValidated[i] = true;
                        _tPlayerInfo[i].text = "PRESS \"A\" WHEN ALL PLAYERS ARE READY";
                    }
                    else
                    {
                        _tPlayerInfo[i].text = "PRESS \"A\" TO PLAY";
                    }
                }
                else
                {
                    _tPlayerInfo[i].text = "PAD DISCONNECTED";
                    _bValidated[i] = false;
                    _bConfirmed[i] = false;
                }
            }

            if( iPlayers >= 2 && iConfirmed == iPlayers )
            {
                _bSelectionFinalize = true;
                GameSettings._iNbPlayers = iPlayers;

                List<PlayerColor> tAvailableColors = new List<PlayerColor>();
                List<PlayerColor> tColors = new List<PlayerColor>();

                for( int i = 0; i < iPlayers; i++ ) {
                    tAvailableColors.Add( (PlayerColor)i );
                }

                for( int i = 0; i < iPlayers; i++ )
                {
                    int iColor = Random.Range( 0, tAvailableColors.Count );
                    Debug.Log( iColor + " | " + tAvailableColors.Count ) ;
                    tColors.Add( tAvailableColors[iColor] );
                    tAvailableColors.RemoveAt( iColor );

                    _tPlayerInfo[i].transform.parent.GetComponent<Image>().color = GameSettings.Instance._tPlayerColors[ tColors[i] ];
                }

                GameSettings.Instance._tColorOrder = tColors;
            }
        }
    }

    public void GoToMainMenu()
    {
        DisableAll();
        _tMainMenuRoot.SetActive( true );
        _PreviousMenu = null;
    }


    public void GoToPlayerSelection()
    {
        DisableAll();
        _tPlayerSelectionRoot.SetActive( true );
        _PreviousMenu = GoToMainMenu;
    }

    public void GoToCredits()
    {
        DisableAll();
        _tCreditsRoot.SetActive( true );
        _PreviousMenu = GoToMainMenu;
    }

    public void Back()
    {
        if( _PreviousMenu != null )
        {
            DisableAll();
            _PreviousMenu();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DisableAll()
    {
        _tSplashRoot.SetActive( false );
        _tMainMenuRoot.SetActive( false );
        _tCreditsRoot.SetActive( false );
        _tPlayerSelectionRoot.SetActive( false );
    }

    public void LoadLevel( string sLevelName )
    {
        DisableAll();
        StartCoroutine( LoadLevelManager( sLevelName ) );
    }

    public IEnumerator LoadLevelManager( string sName )
    {
        AsyncOperation tLoading = SceneManager.LoadSceneAsync( sName );
        tLoading.allowSceneActivation = true;
        _tLoadingRoot.SetActive(true);

        Slider tSlider = _tLoadingRoot.transform.FindChild( "Slider" ).GetComponent<Slider>();

        while (!tLoading.isDone)
        {
            tSlider.value = tLoading.progress;
            yield return new WaitForFixedUpdate();
        }

        _tLoadingRoot.SetActive(false);

        if( EventSystem.current != null )
        {
            if( EventSystem.current.firstSelectedGameObject != null ) {
                EventSystem.current.firstSelectedGameObject.GetComponent<Selectable>().Select();
            }
        }
    }
}
