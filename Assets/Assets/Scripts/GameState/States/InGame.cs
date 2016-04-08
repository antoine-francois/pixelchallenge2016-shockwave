using UnityEngine;
using System.Collections.Generic;

public class InGame : State
{

    public override void EnterState(State tPrevious)
    {
        MenuGameState.Instance._tMenuManager.UpdateHUD();
    }

    public override State UpdateState()
    {
        PlayerManager.Instance.Update();
        MenuManager.Instance.UpdateHUD();
        return null;
    }

    public override void ExitState( State tNext )
    {
    }
}
