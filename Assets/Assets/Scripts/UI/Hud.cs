using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Hud : MonoBehaviour
{
    [Header("HUD")]
    public List<GameObject> _tScoreList;
    public Text _tTimer;
    public Text _tTimeOut;
    public Text _tCurrentPlayer;

    [Header("Pause")]
    public GameObject _tPauseRoot;

    void Start()
    {
        TogglePause( false );
        for( int i = 0; i < _tScoreList.Count; i++ ) {
            _tScoreList[i].SetActive( ( i < GameSettings._iNbPlayers ) ? true : false );
        }
    }

    void Update()
    {
        if( _tPauseRoot.activeSelf && Input.GetButtonUp( "Submit" ) )
        {
            PointerEventData tPointer = new PointerEventData(EventSystem.current); // pointer event for Execute
            ExecuteEvents.Execute( EventSystem.current.currentSelectedGameObject, tPointer, ExecuteEvents.submitHandler );
        }

        PlayerColor eCurrentPlayer = PlayerManager.Instance._eCurrentPlayer;
        PlayerState eState = PlayerManager.Instance._tPlayers[eCurrentPlayer]._eState;

        for( int i = 0; i < GameSettings._iNbPlayers; i++ )
        {
            PlayerColor eColor = (PlayerColor)i;
            _tScoreList[i].GetComponentInChildren<Text>().text = PlayerManager.Instance._tPlayers[eColor]._iScore.ToString();
        }

        _tTimer.text = PlayerManager.Instance.GetChrono();
        _tTimeOut.gameObject.SetActive( eState == PlayerState.Timeout );

        _tCurrentPlayer.gameObject.SetActive( eState == PlayerState.Intro );
        _tCurrentPlayer.text = "Player " + eCurrentPlayer.ToString();

        if( Input.GetKeyDown( KeyCode.JoystickButton7 ) )
        {
            TogglePause( true );
        }
    }

    public void TogglePause( bool bState )
    {
        GameSettings.Instance._bPause = bState;
        _tPauseRoot.SetActive( bState );
        Time.timeScale = ( bState ) ? 0f : 1f;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene( "MenuScene" );
    }
}
