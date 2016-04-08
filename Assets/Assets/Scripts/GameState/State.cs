using UnityEngine;
using System.Collections;

public class State
{
    public virtual void EnterState( State tPrevious )
    {}

    public virtual State UpdateState()
    {
        return null;
    }

    public virtual void ExitState( State tNext )
    {}
}
