using UnityEngine;
using System.Collections;

public class Player
{
	public int _iID;
    public int _iScore;

    private Vector2 _tPlayerPos = new Vector2( 0f, 0f );
    private const float CAMERA_SPEED = 0.5f;

    public Player( int iId )
    {
        _iID = iId;
        _iScore = 0;
    }

    public bool Update()
    {
        _tPlayerPos += new Vector2( Joystick.GetAxis( "LeftX", 0 ), - Joystick.GetAxis( "LeftY", 0 ) ) * CAMERA_SPEED;
        Camera.main.transform.position = new Vector3( _tPlayerPos.x, Camera.main.transform.position.y, _tPlayerPos.y );
        
        if( Joystick.GetButtonDown( "A", 0 ) )
        {
            Debug.Log( "Shockwave" );
            return true;
        }
        return false;
    }
}
