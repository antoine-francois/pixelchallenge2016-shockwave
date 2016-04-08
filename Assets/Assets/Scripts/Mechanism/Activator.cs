using UnityEngine;
using System.Collections.Generic;

public class Activator : MonoBehaviour
{
    public List<GenericMechanism> _tMechaList;

    public void OnTriggerEnter( Collider tCollider )
    {
        if( tCollider.tag == "Ball" )
        {
            for( int i = 0; i < _tMechaList.Count; i++ )
            {
                if( _tMechaList[i] != null )
                {
                    _tMechaList[i].Activate( tCollider.GetComponent<Ball>() );
                }
            }
        }
    }
}
