using UnityEngine;
using System.Collections;

public class PlayerDataManager
{
    public struct PlayerData
    {
        public int _iID;
        public int _iScore;
    }

    static private PlayerDataManager _sInstance;
    static public PlayerDataManager Instance
    {
        get
        {
            if (_sInstance == null)
                _sInstance = new PlayerDataManager();

            return _sInstance;
        }

        private set {}
    }

    public PlayerData[] _tPlayers;
    public int _iCurrentPlayer = 0;

    private PlayerDataManager()
    {
        _tPlayers = new PlayerData[4];

        for( int i = 0; i < _tPlayers.Length; i++ )
        {
            _tPlayers[i]._iID = i;
            _tPlayers[i]._iScore = 0;
        }
    }
}
