using UnityEngine;
using System.Collections.Generic;

public class InGame : State
{

    public override void EnterState(State tPrevious)
    {
    }

    public override State UpdateState()
    {
        PlayerManager.Instance.Update();
        return null;
    }

    public override void ExitState( State tNext )
    {
    }
}
