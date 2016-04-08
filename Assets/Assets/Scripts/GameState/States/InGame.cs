using UnityEngine;
using System.Collections.Generic;

public class InGame : State
{
    private List<Vector2> _tPlayerPos = new List<Vector2>();

    private int _iCurrentPlayer = 0;
    private float _fPlayerTimer = 0f;

    private const float MAX_TIME = 8f;
    private const float CAMERA_SPEED = 0.5f;

    public override void EnterState(State tPrevious)
    {
        for( int i = 0; i < GameSettings._iNbPlayers; i++ ) {
            _tPlayerPos.Add( new Vector3( 0f, 0f ) );
        }
        string[] sJoystickName = Input.GetJoystickNames();
        for( int i = 0; i < sJoystickName.Length; i++ )
        {
            Debug.Log( sJoystickName[i] );
        }
    }

    public override State UpdateState()
    {
        _tPlayerPos[_iCurrentPlayer] += new Vector2( Joystick.GetAxis( "LeftX", 0 ), - Joystick.GetAxis( "LeftY", 0 ) ) * CAMERA_SPEED;
        Camera.main.transform.position = new Vector3( _tPlayerPos[_iCurrentPlayer].x, Camera.main.transform.position.y, _tPlayerPos[_iCurrentPlayer].y );

        _fPlayerTimer += Time.deltaTime;

        if( _fPlayerTimer > MAX_TIME )
        {
            NextPlayer();
        }
        else if( Joystick.GetButtonDown( "A", 0 ) )
        {
            Debug.Log( "Shockwave" );
        }

        return null;
    }

    void NextPlayer()
    {
        _fPlayerTimer = 0f;
        _iCurrentPlayer++;

        if( _iCurrentPlayer == GameSettings._iNbPlayers ) {
            _iCurrentPlayer = 0;
        }
        Debug.Log( "Player: " + _iCurrentPlayer );
    }

    public override void ExitState(State tNext)
    {
    }
}
