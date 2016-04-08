using System;
using UnityEngine;
using System.Collections.Generic;

public delegate void BallActionCallback( Ball tBall );

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }

    private List<Ball> _tBallList = new List<Ball>();

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
}
