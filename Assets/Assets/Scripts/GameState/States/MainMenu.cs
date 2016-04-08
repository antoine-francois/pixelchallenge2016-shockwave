using UnityEngine;
using System.Collections;

public class MainMenu : State
{
    public override void EnterState(State tPrevious)
    {
        MenuGameState.Instance._tMenuManager.GoToPressStart();
    }

    public override State UpdateState()
    {
        return null;
    }

    public override void ExitState(State tNext)
    {
    }
}