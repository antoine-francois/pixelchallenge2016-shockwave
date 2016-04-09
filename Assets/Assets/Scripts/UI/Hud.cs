using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Hud : MonoBehaviour
{
    [Header("HUD")]
    public List<Text> _tScoreList;
    public Text _tTimer;
    public Text _tTimeOut;
    public Text _tCurrentPlayer;

    [Header("Pause")]
    public GameObject _tPauseRoot;

    void Start()
    {
        TogglePause( false );
        for( int i = 0; i < _tScoreList.Count; i++ )
        {
            _tScoreList[i].gameObject.SetActive( ( i < GameSettings._iNbPlayers ) ? true : false );
        }
    }

    void Update()
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

        if( Input.GetKeyDown( KeyCode.JoystickButton7 ) )
        {
            TogglePause( true );
        }
    }

    public void TogglePause( bool bState )
    {
        _tPauseRoot.SetActive( bState );
        Time.timeScale = ( bState ) ? 0f : 1f;
    }
}
