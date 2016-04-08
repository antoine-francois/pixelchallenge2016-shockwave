using UnityEngine;
using System.Collections.Generic;

public class MechanismManager : MonoBehaviour
{
    public static MechanismManager Instance { get; private set; }

    [HideInInspector]
    public List<GenericMechanism> _tMechaList = new List<GenericMechanism>();

	void Awake()
    {
        Instance = this;
	}

    public void RegisterMechanism( GenericMechanism tMecha )
    {
        _tMechaList.Add( tMecha );
    }

    public void RemoveMechanism( GenericMechanism tMecha )
    {
        if( _tMechaList.Contains( tMecha ) ) {
            _tMechaList.Remove( tMecha );
        }
    }

    public void NextTurn()
    {
        for( int i = 0; i < _tMechaList.Count; i++ ) {
            _tMechaList[i].NextTurn();
        }
    }
}
