using UnityEngine;
using System.Collections;

public class StateManager
{
    State _tCurrentState;

    public void Start( State tStartState )
    {
        _tCurrentState = tStartState;
        _tCurrentState.EnterState(null);
    }

    public void Update()
    {
        State tNew = _tCurrentState.UpdateState();

        if (tNew != null)
            ChangeState(tNew);
    }

    public void Exit()
    {
        _tCurrentState.ExitState(null);
        _tCurrentState = null;
    }

    public void ChangeState( State tNew )
    {
        _tCurrentState.ExitState(tNew);
        tNew.EnterState(_tCurrentState);
        _tCurrentState = tNew;
    }
}
