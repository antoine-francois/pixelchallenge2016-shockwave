using UnityEngine;
using System.Collections;

public class InGame : State
{
    private int _iCurrentPlayer = 0;
    private float _fPlayerTimer = 0f;

    private const float MAX_TIME = 8f;

    public override void EnterState(State tPrevious)
    {
    }

    public override State UpdateState()
    {


        _fPlayerTimer += Time.deltaTime;

        if( _fPlayerTimer > MAX_TIME )
        {

        }


        _fPlayerTimer = 0f;
        _iCurrentPlayer++;
        if( _iCurrentPlayer == GameSettings._iNbPlayers ) {
            _iCurrentPlayer = 0;
        }
        Debug.Log( "Player: " + _iCurrentPlayer );

        return null;
    }

    public override void ExitState(State tNext)
    {
    }
}
