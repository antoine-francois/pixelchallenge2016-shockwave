using UnityEngine;
using System.Collections;


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

    private float _fShockwaveRadius;
    private float _fShockwavePower;

    private GameObject _tShockwavePreview;
    private Material _tShockwaveMaterial;

    public PlayerState _eState = PlayerState.Intro;

    private Vector2 _tPlayerPos = new Vector2( 0f, 0f );
    private const float CAMERA_SPEED = 0.25f;

    private const float SHOCKWAVE_RADIUS_INC = 0.05f;
    private const float SHOCKWAVE_POWER_INC = 0.1f;

    private const float SHOCKWAVE_MAX_RADIUS = 5f;
    private const float SHOCKWAVE_MAX_POWER = 25f;

    public Player( int iId )
    {
        _iID = iId;
        _iScore = 0;
    }

    public bool Update()
    {
        if( _eState == PlayerState.Play )
        {
            _tPlayerPos += new Vector2( Joystick.GetAxis( "LeftX", 0 ), - Joystick.GetAxis( "LeftY", 0 ) ) * CAMERA_SPEED;
            Camera.main.transform.position = new Vector3( _tPlayerPos.x, Camera.main.transform.position.y, _tPlayerPos.y );
        
            if( Joystick.GetButtonDown( "A", 0 ) )
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
            _fShockwavePower += SHOCKWAVE_POWER_INC;
            _fShockwavePower = Mathf.Clamp( _fShockwavePower, 0.1f, SHOCKWAVE_MAX_POWER );

            _fShockwaveRadius += SHOCKWAVE_RADIUS_INC;
            _fShockwaveRadius = Mathf.Clamp( _fShockwaveRadius, 1f, SHOCKWAVE_MAX_RADIUS );


            _tShockwavePreview.transform.localScale = new Vector3( _fShockwaveRadius, _fShockwaveRadius, _fShockwaveRadius );

            Color tColor = _tShockwaveMaterial.GetColor( "_Color" );
            _tShockwaveMaterial.SetColor( "_Color", new Color( tColor.r, tColor.g, tColor.b, _fShockwavePower / SHOCKWAVE_MAX_POWER / 2f ) );

            if( Joystick.GetButtonUp( "A", 0 ) )
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
