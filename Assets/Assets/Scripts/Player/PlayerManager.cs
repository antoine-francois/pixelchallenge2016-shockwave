using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    static public PlayerManager Instance { get; private set; }

    public Dictionary<PlayerColor, Player> _tPlayers = new Dictionary<PlayerColor, Player>();

    [HideInInspector]
    public PlayerColor _eCurrentPlayer = PlayerColor.Red;

    private float _fTimer = 0f;
    private float _fPlayTimer = 0f;

    private int _iTurnCount = 1;
    public int TurnCount { get { return _iTurnCount; } }

    public const float MAX_TIME = 30f;
    public const float TIME_OUT = 1.5f;
    public const float INTRO_TIME = 1.5f;

    void Awake()
    {
        Instance = this;

        for( int i = 0; i < GameSettings._iNbPlayers; i++ ) {
            _tPlayers.Add( GameSettings.Instance._tColorOrder[i], new Player( i ) );
        }

        StartCoroutine( _tPlayers[_eCurrentPlayer].ControllerVibration( 0.6f, 0.25f ) );
    }

    void Update()
    {
        switch( _tPlayers[_eCurrentPlayer]._eState )
        {
            case PlayerState.Play:
                _fPlayTimer += Time.deltaTime;
                if( _fPlayTimer > MAX_TIME ) {
                    _tPlayers[_eCurrentPlayer]._eState = PlayerState.Timeout;
                }
                break;

            case PlayerState.Timeout:
                _fTimer += Time.deltaTime;
                if( _fTimer > TIME_OUT ) {
                    NextPlayer();
                }
                return;

            case PlayerState.Intro:
                _fTimer += Time.deltaTime;
                if( _fTimer > INTRO_TIME )
                {
                    _fTimer = 0f;
                    _tPlayers[_eCurrentPlayer]._eState = PlayerState.Play;
                }
                return;
        }

        if( _tPlayers[_eCurrentPlayer].Update() ) {
            NextPlayer();
        }
    }

    public void NextPlayer()
    {

        _fPlayTimer = 0f;
        _fTimer = 0f;
        _eCurrentPlayer = (PlayerColor)( _iTurnCount % GameSettings._iNbPlayers );

        StartCoroutine( _tPlayers[_eCurrentPlayer].ControllerVibration( 0.6f, 0.25f ) );

        _tPlayers[_eCurrentPlayer]._eState = PlayerState.Intro;

        _iTurnCount++;
        MechanismManager.Instance.NextTurn();
    }

    public string GetChrono()
    {
        TimeSpan tTime = TimeSpan.FromSeconds( Mathf.Clamp( MAX_TIME - _fPlayTimer, 0f, MAX_TIME ) );
        return string.Format( "{0:00}.{1:000}", tTime.Seconds, tTime.Milliseconds );
    }
}
