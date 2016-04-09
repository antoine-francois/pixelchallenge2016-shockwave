using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public delegate void BallActionCallback( Ball tBall );

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }

    public List<GameObject> _tBallPrefab = new List<GameObject>();
    private List<Ball> _tBallList = new List<Ball>();

    public int _iNbBalls { get; private set; }

    private const float MIN_VELOCITY_SQR = 0.04f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _iNbBalls = transform.childCount;
        int iPlayers = GameSettings._iNbPlayers;
        for( int i = 0; i < _iNbBalls; i++ )
        {
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

    public int GetScore( PlayerColor eColor )
    {
        return _iNbBalls / GameSettings._iNbPlayers - _tBallList.Count( c => c._ePlayer == eColor );
    } 
}
