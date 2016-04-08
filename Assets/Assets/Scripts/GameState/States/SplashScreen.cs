using UnityEngine;
using System.Collections;

public class SplashScreen : State
{
    float _fCurrentTimer = 0.0f;
    float _fFadeSpeed = 1.0f;
    float _fWaitTime = 5.0f;
    MenuManager _tMenuManager;

    public override void EnterState(State tPrevious)
    {
        _tMenuManager = MenuGameState.Instance._tMenuManager;
    }

    public override State UpdateState()
    {
        _fCurrentTimer += Time.deltaTime;

        float fFade = Mathf.Clamp01(Mathf.Sin(_fCurrentTimer * Mathf.PI / _fWaitTime) * _fFadeSpeed);

        _tMenuManager._tSplashFade.color = new Color(0, 0, 0, fFade);

        if (_fCurrentTimer >= 1.0f || Input.anyKeyDown)
        {
            return new MainMenu();
        }

        return null;
    }

    public override void ExitState(State tNext)
    {

    }
}
