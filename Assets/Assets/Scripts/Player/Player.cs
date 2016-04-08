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

    public PlayerState _eState = PlayerState.Intro;

    private Vector2 _tPlayerPos = new Vector2( 0f, 0f );
    private const float CAMERA_SPEED = 0.25f;

    private const float SHOCKWAVE_RADIUS_INC = 0.05f;
    private const float SHOCKWAVE_POWER_INC = 0.1f;

    private LineData _tWavePreview;

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

                _tWavePreview = new LineData();
                SetWavePreview();
                DrawLine.Instance.AddLineData( _tWavePreview );
            }
        }
        else if( _eState == PlayerState.ChargeShockwave )
        {
            _fShockwavePower += SHOCKWAVE_POWER_INC;
            _fShockwaveRadius += SHOCKWAVE_RADIUS_INC;

            SetWavePreview();

            if( Joystick.GetButtonUp( "A", 0 ) )
            {
                ShockwaveFactory.Instance.CreateShockwave( _fShockwavePower, _fShockwaveRadius );
                _eState = PlayerState.Shockwave;
                DrawLine.Instance.RemoveLineData( _tWavePreview );
                _tWavePreview = null;
            }
        }
        else if( _eState == PlayerState.Shockwave && !BallManager.Instance.IsBallMoving() )
        {
            return true;
        }
        return false;
    }

    void SetWavePreview()
    {
        if( _tWavePreview == null ) {
            return;
        }

        _tWavePreview._tPosList = DrawLine.GetCircle( new Vector3( _tPlayerPos.x, 0f, _tPlayerPos.y ), _fShockwaveRadius, 0.1f );
        _tWavePreview._tColor = Color.yellow;
    }
}
