using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [Header("Spash screens")]
    public Image _tSplash;
    public Image _tSplashFade;

    [Header("HUD")]
    public List<Text> _tScoreList;
    public Text _tCurrentPlayer;
    public Text _tTimeOut;
    public Text _tCurrentPlayerIntro;

    [Header("Menus Root")]
    public GameObject _tSplashRoot;
    public GameObject _tPressStartRoot;
    public GameObject _tMainMenuRoot;
    public GameObject _tCreditsRoot;
    public GameObject _tHUDRoot;
    public GameObject _tPauseRoot;
    public GameObject _tLoadingRoot;

    public delegate void BackCallback();
    BackCallback _PreviousMenu = null;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if( Input.GetKeyDown( KeyCode.Escape ) )
        {
            Back();
        }
    }

    public void UpdateHUD()
    {
        for( int i = 0; i < GameSettings._iNbPlayers; i++ ) {
            _tScoreList[i].text =  string.Format( "P{0}\n{1}", i + 1, PlayerManager.Instance._tPlayers[i]._iScore );
        }

        int iCurrentPlayer = PlayerManager.Instance._iCurrentPlayer;
        _tCurrentPlayer.text = "Current Player : " + ( iCurrentPlayer + 1 );

        PlayerState eState = PlayerManager.Instance._tPlayers[iCurrentPlayer]._eState;
        _tTimeOut.gameObject.SetActive( eState == PlayerState.Timeout );

        _tCurrentPlayerIntro.gameObject.SetActive( eState == PlayerState.Intro );
        _tCurrentPlayerIntro.text = "Player " + ( iCurrentPlayer + 1 );
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
