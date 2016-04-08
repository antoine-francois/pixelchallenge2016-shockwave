using System;
using UnityEngine;
using System.Collections.Generic;

public delegate void BallActionCallback( Ball tBall );

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }

    private List<Ball> _tBallList = new List<Ball>();

    private const float MIN_VELOCITY_SQR = 0.01f;

    void Awake()
    {
        Instance = this;
    }

    public void AddBall( Ball tBall )
    {
        _tBallList.Add( tBall );
    }

    public void RemoveBall( Ball tBall )
    {
        if( _tBallList.Contains( tBall ) ) {
            _tBallList.Remove( tBall );
        }
    }

    public void DoStuffOnEachBalls( BallActionCallback tDelegate )
    {
        for( int i = 0; i < _tBallList.Count; i++ )
        {
            tDelegate( _tBallList[i] );
        }
    }

    public bool IsBallMoving()
    {
        bool bMove = false;
        for( int i = 0; i < _tBallList.Count; i++ )
        {
            if( _tBallList[i]._tRigidbody.velocity.sqrMagnitude > MIN_VELOCITY_SQR ) {
                bMove = true;
            }
            else
            {
                _tBallList[i]._tRigidbody.velocity = Vector3.zero;
                _tBallList[i]._tRigidbody.angularVelocity = Vector3.zero;
            }
        }
        return bMove;
    }
}
