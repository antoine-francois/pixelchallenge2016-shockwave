using UnityEngine;
using System.Collections;

public abstract class GenericMechanism : MonoBehaviour
{
    public bool _bActive = false;
    public int _iAcivationTurn = 4;

    private int _iCurrentTurn = 0;

	public virtual void Start()
    {
        MechanismManager.Instance.RegisterMechanism( this );
	}

    public virtual void Activate( Ball tBall )
    {
        _bActive = true;
        _iCurrentTurn = 0;
    }

    public virtual void Disable()
    {
        _bActive = false;
    }

    void OnDestroy()
    {
        MechanismManager.Instance.RemoveMechanism( this );
    }

    public void NextTurn()
    {
        if( _bActive )
        {
            _iCurrentTurn++;
            if( _iCurrentTurn > _iAcivationTurn ) {
                Disable();
            }
        }
    }
}
