using UnityEngine;
using System.Collections;
using XInputDotNetPure;


public enum PlayerState
{
    Intro,
    Play,
    ChargeShockwave,
    Shockwave,
    Timeout
}

public class Player
{
	public int _iID;
    public int _iScore;

    // XInput stuff
    private PlayerIndex _ePlayerIndex;
    public GamePadState _tState;
    public GamePadState _tPrevState;

    private float _fShockwaveRadius;
    private float _fShockwavePower;

    private GameObject _tShockwavePreview;
    private Material _tShockwaveMaterial;

    public PlayerState _eState = PlayerState.Intro;

    private const float CAMERA_SPEED = 0.25f;

    private const float SHOCKWAVE_RADIUS_INC = 0.05f * 60f;
    private const float SHOCKWAVE_POWER_INC = 0.1f * 60f;

    private const float SHOCKWAVE_MAX_RADIUS = 10f;
    private const float SHOCKWAVE_MAX_POWER = 30f;

    public Player( int iId )
    {
        _iID = iId;
        _iScore = 0;

        switch( _iID + 1 )
        {
            case 1:
                _ePlayerIndex = PlayerIndex.One;
                break;
            case 2:
                _ePlayerIndex = PlayerIndex.Two;
                break;
            case 3:
                _ePlayerIndex = PlayerIndex.Three;
                break;
            case 4:
                _ePlayerIndex = PlayerIndex.Four;
                break;
        }
    }


    public bool Update()
    {
        GamePadState tState = GamePad.GetState( _ePlayerIndex );
        if( tState.IsConnected )
        {
            _tPrevState = _tState;
            _tState = tState;
        }
        else
        {
            Debug.LogError( "Pad " + _ePlayerIndex.ToString() + " not connected !" );
        }

        if( _eState == PlayerState.Play )
        {        
            if( Joystick.GetButtonDown( XInputKey.A, _tState, _tPrevState ) )
            {
                _eState = PlayerState.ChargeShockwave;
                _fShockwavePower = 1f;
                _fShockwaveRadius = 0.1f;

                _tShockwavePreview = ShockwaveFactory.Instance.CreateShockwavePreview( _fShockwaveRadius );
                _tShockwaveMaterial = _tShockwavePreview.GetComponentInChildren<MeshRenderer>().materials[0];
            }
        }
        else if( _eState == PlayerState.ChargeShockwave )
        {
            _fShockwavePower += SHOCKWAVE_POWER_INC * Time.deltaTime;
            _fShockwavePower = Mathf.Clamp( _fShockwavePower, 0.1f, SHOCKWAVE_MAX_POWER );

            _fShockwaveRadius += SHOCKWAVE_RADIUS_INC * Time.deltaTime;
            _fShockwaveRadius = Mathf.Clamp( _fShockwaveRadius, 1f, SHOCKWAVE_MAX_RADIUS );


            _tShockwavePreview.transform.localScale = new Vector3( _fShockwaveRadius, _fShockwaveRadius, _fShockwaveRadius );

            Color tColor = _tShockwaveMaterial.GetColor( "_Color" );
            _tShockwaveMaterial.SetColor( "_Color", new Color( tColor.r, tColor.g, tColor.b, _fShockwavePower / SHOCKWAVE_MAX_POWER / 2f ) );

            if( Joystick.GetButtonUp( XInputKey.A, _tState, _tPrevState ) )
            {
                GameObject.Destroy( _tShockwavePreview );
                ShockwaveFactory.Instance.CreateShockwave( _fShockwavePower, _fShockwaveRadius );
                _eState = PlayerState.Shockwave;
            }
        }
        else if( _eState == PlayerState.Shockwave && !BallManager.Instance.IsBallMoving() )
        {
            return true;
        }
        return false;
    }
}
