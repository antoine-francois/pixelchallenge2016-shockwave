using UnityEngine;
using System.Collections.Generic;

public enum PlayerColor
{
    Red,
    Blue,
    Green,
    Yellow
}

public class GameSettings
{
    private static readonly GameSettings _tInstance = new GameSettings();
    public static GameSettings Instance { get { return _tInstance; } }

    public static int _iNbPlayers = 2;
    public static string _sMap = "Default";

    public Dictionary<PlayerColor, Color> _tPlayerColors = new Dictionary<PlayerColor, Color>();
    public List<PlayerColor> _tColorOrder;

    public bool _bPause = false;

    private GameSettings()
    {
        _tPlayerColors.Add( PlayerColor.Red, new Color( 1f, 0f, 0f, 1f ) );
        _tPlayerColors.Add( PlayerColor.Blue, new Color( 0f, 0.584f, 1f, 1f ) );
        _tPlayerColors.Add( PlayerColor.Green, new Color( 0.161f, 0.992f, 0.051f, 1f ) );
        _tPlayerColors.Add( PlayerColor.Yellow, new Color( 0.82f, 1f, 0f, 1f ) );
    }
}
