using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerManager
{
    static private PlayerManager _sInstance;
    static public PlayerManager Instance
    {
        get
        {
            if (_sInstance == null)
                _sInstance = new PlayerManager();

            return _sInstance;
        }

        private set {}
    }

    public List<Player> _tPlayers = new List<Player>();
    public int _iCurrentPlayer = 0;

    private float _fTimer = 0f;
    private float _fPlayTimer = 0f;

    public const float MAX_TIME = 30f;
    public const float TIME_OUT = 1.5f;
    public const float INTRO_TIME = 1.5f;

    private PlayerManager()
    {
        for( int i = 0; i < GameSettings._iNbPlayers; i++ )
        {
            _tPlayers.Add( new Player( i ) );
        }
    }

    public bool Update()
    {
        switch( _tPlayers[_iCurrentPlayer]._eState )
        {
            case PlayerState.Play:
                _fPlayTimer += Time.deltaTime;
                if( _fPlayTimer > MAX_TIME ) {
                    _tPlayers[_iCurrentPlayer]._eState = PlayerState.Timeout;
                }
                break;

            case PlayerState.Timeout:
                _fTimer += Time.deltaTime;
                if( _fTimer > TIME_OUT ) {
                    return true;
                }
                return false;

            case PlayerState.Intro:
                _fTimer += Time.deltaTime;
                if( _fTimer > INTRO_TIME )
                {
                    _fTimer = 0f;
                    _tPlayers[_iCurrentPlayer]._eState = PlayerState.Play;
                }
                return false;
        }

        return _tPlayers[_iCurrentPlayer].Update();
    }

    public void NextPlayer()
    {
        _fPlayTimer = 0f;
        _fTimer = 0f;
        _iCurrentPlayer++;

        if( _iCurrentPlayer == GameSettings._iNbPlayers ) {
            _iCurrentPlayer = 0;
        }

        _tPlayers[_iCurrentPlayer]._eState = PlayerState.Intro;
    }

    public string GetChrono()
    {
        TimeSpan tTime = TimeSpan.FromSeconds( Mathf.Clamp( MAX_TIME - _fPlayTimer, 0f, MAX_TIME ) );
        return string.Format( "{0:00}.{1:000}", tTime.Seconds, tTime.Milliseconds );
    }
}
