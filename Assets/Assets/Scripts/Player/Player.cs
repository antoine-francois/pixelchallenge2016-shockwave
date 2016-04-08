using UnityEngine;
using System.Collections;


public enum PlayerState
{
    Intro,
    Play,
    Shockwave,
    Timeout
}

public class Player
{
	public int _iID;
    public int _iScore;

    public PlayerState _eState = PlayerState.Intro;

    private Vector2 _tPlayerPos = new Vector2( 0f, 0f );
    private const float CAMERA_SPEED = 0.25f;

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
                ShockwaveFactory.Instance.CreateShockwave();
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
