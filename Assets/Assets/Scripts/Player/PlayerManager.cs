using UnityEngine;
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

    private float _fPlayerTimer = 0f;
    private const float MAX_TIME = 8f;

    private PlayerManager()
    {
        for( int i = 0; i < GameSettings._iNbPlayers; i++ )
        {
            _tPlayers.Add( new Player( i ) );
        }
    }

    public void Update()
    {
        _fPlayerTimer += Time.deltaTime;

        if( ( _fPlayerTimer > MAX_TIME && _tPlayers[_iCurrentPlayer]._eState == PlayerState.Play ) || _tPlayers[_iCurrentPlayer].Update() )
        {
            _fPlayerTimer = 0f;
            _iCurrentPlayer++;

            if( _iCurrentPlayer == GameSettings._iNbPlayers ) {
                _iCurrentPlayer = 0;
            }

            _tPlayers[_iCurrentPlayer]._eState = PlayerState.Play;
            Debug.Log( "Player: " + _iCurrentPlayer );
        }
    }
}
