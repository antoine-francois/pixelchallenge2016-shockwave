using System;
using UnityEngine;
using System.Collections.Generic;

public delegate void BallActionCallback( Ball tBall );

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }

    public List<GameObject> _tBallPrefab = new List<GameObject>();
    private List<Ball> _tBallList = new List<Ball>();

    private const float MIN_VELOCITY_SQR = 0.01f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        int iPlayers = GameSettings._iNbPlayers;
        for( int i = 0; i < transform.childCount; i++ )
        {
            Debug.Log( ( iPlayers + i ) % iPlayers );
            GameObject tBall = Instantiate( _tBallPrefab[ ( iPlayers + i ) % iPlayers ] );
            tBall.transform.SetParent( transform.GetChild(i) );
            tBall.transform.localPosition = new Vector3( 0f, 2f, 0f );
        }
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
