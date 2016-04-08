using UnityEngine;
using System.Collections.Generic;

public class Activator : MonoBehaviour
{
    public List<GenericMechanism> _tMechaList;
    public Transform _tPlate;
    public float _fHeight;
    public MeshRenderer _tBorderRenderer;
    public MeshRenderer _tPlateRenderer;
    public Color _tEnabled;
    public Color _tDisabled;

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

            EnableButton();
        }
    }

    public void OnTriggerExit(Collider tCollider)
    {
        if (tCollider.tag == "Ball")
        {
            DisableButton();
        }
    }

    void EnableButton()
    {
        _tPlate.localPosition = Vector3.zero;
        _tPlateRenderer.material.SetColor("_EmissiveColor", _tEnabled);
        _tBorderRenderer.material.SetColor("_EmissiveColor", _tEnabled);

    }

    void DisableButton()
    {
        _tPlate.localPosition = Vector3.up * _fHeight;
        _tPlateRenderer.material.SetColor("_EmissiveColor", _tDisabled);
        _tBorderRenderer.material.SetColor("_EmissiveColor", _tDisabled);

    }
}
