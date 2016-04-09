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

    [Header("HUD")]
    public List<Text> _tScoreList;
    public Text _tTimer;
    public Text _tTimeOut;
    public Text _tCurrentPlayer;

    [Header("Player Selection")]
    public GameObject _tPlayerRoot;
    public List<Text> _tPlayerInfo = new List<Text>();
    private bool[] _bConnected = new bool[4];
    private bool[] _bValidated = new bool[4];
    private bool[] _bConfirmed = new bool[4];

    private GamePadState[] _tPadState = new GamePadState[4];

    [Header("Menus Root")]
    public GameObject _tSplashRoot;
    public GameObject _tPressStartRoot;
    public GameObject _tMainMenuRoot;
    public GameObject _tPlayerSelectionRoot;
    public GameObject _tCreditsRoot;
    public GameObject _tHUDRoot;
    public GameObject _tPauseRoot;
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
    }

    void Update()
    {
        if( Input.GetKeyDown( KeyCode.Escape ) ) {
            Back();
        }

        if( _tPlayerSelectionRoot.activeSelf )
        {
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
                GameSettings._iNbPlayers = iPlayers;
                LoadLevel( "MapScene" );
            }
        }
    }

    public void UpdateHUD()
    {
        int iCurrentPlayer = PlayerManager.Instance._iCurrentPlayer;
        PlayerState eState = PlayerManager.Instance._tPlayers[iCurrentPlayer]._eState;
        for( int i = 0; i < GameSettings._iNbPlayers; i++ ) {
            _tScoreList[i].text =  string.Format( "P{0}\n{1}", i + 1, PlayerManager.Instance._tPlayers[i]._iScore );
        }

        _tTimer.text = PlayerManager.Instance.GetChrono();

        _tTimeOut.gameObject.SetActive( eState == PlayerState.Timeout );

        _tCurrentPlayer.gameObject.SetActive( eState == PlayerState.Intro );
        _tCurrentPlayer.text = "Player " + ( iCurrentPlayer + 1 );
    }

    public void GoToPressStart()
    {
        DisableAll();
        _tPressStartRoot.SetActive(true);
        _PreviousMenu = null;
    }

    public void GoToMainMenu()
    {
        DisableAll();
        _tMainMenuRoot.SetActive(true);
        _PreviousMenu = GoToPressStart;
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
        _tCreditsRoot.SetActive(true);
        _PreviousMenu = GoToMainMenu;
    }

    public void GoToHUD()
    {
        DisableAll();
        _tHUDRoot.SetActive(true);
        _PreviousMenu = GoToPause;
    }

    public void GoToPause()
    {
        DisableAll();
        _tPauseRoot.SetActive(true);
        _PreviousMenu = GoToHUD;
    }

    public void Back()
    {
        if (_PreviousMenu != null)
        {
            DisableAll();
            _PreviousMenu();
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void QuitGame()
    {
        StartCoroutine(LoadLevelManager("EmptyScene", GoToMainMenu));
        MenuGameState.Instance._tManager.ChangeState(new MainMenu());
    }

    public void DisableAll()
    {
        _tSplashRoot.SetActive(false);
        _tPressStartRoot.SetActive(false);
        _tMainMenuRoot.SetActive(false);
        _tCreditsRoot.SetActive(false);
        _tHUDRoot.SetActive(false);
        _tPauseRoot.SetActive(false);
        _tPlayerSelectionRoot.SetActive(false);
    }

    public void LoadLevel(string LevelName)
    {
        DisableAll();
        MenuGameState.Instance._tManager.ChangeState(new InGame());
        StartCoroutine(LoadLevelManager(LevelName, GoToHUD));
    }

    public IEnumerator LoadLevelManager(string name, BackCallback newMenu)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(name);
        loading.allowSceneActivation = true;
        _tLoadingRoot.SetActive(true);

        Slider slider = _tLoadingRoot.transform.FindChild("Slider").GetComponent<Slider>();

        while (!loading.isDone)
        {
            slider.value = loading.progress;
            yield return new WaitForFixedUpdate();
        }

        _tLoadingRoot.SetActive(false);

        if (EventSystem.current != null)
        {
            if (EventSystem.current.firstSelectedGameObject != null)
                EventSystem.current.firstSelectedGameObject.GetComponent<Selectable>().Select();
        }

        newMenu();
    }
}
