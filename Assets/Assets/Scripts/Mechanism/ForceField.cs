using UnityEngine;
using System.Collections.Generic;

public class ForceField : GenericMechanism
{
    private Collider _tCollider;
    private MeshRenderer _tRenderer;

	public override void Start()
    {
        base.Start();
        _tCollider = GetComponent<CapsuleCollider>();
        _tRenderer = GetComponent<MeshRenderer>();

        Disable();
    }

    public override void Activate( Ball tBall )
    {
        base.Activate( tBall );

        _tCollider.enabled = true;
        _tRenderer.enabled = true;

        gameObject.layer = LayerMask.NameToLayer( "P" + tBall._ePlayer.ToString() );
        GetComponentInChildren<MeshRenderer>().materials[0].SetColor( "_EmissiveColor", GameSettings.Instance._tPlayerColors[tBall._ePlayer] );
    }

    public override void Disable()
    {
        base.Disable();

        _tCollider.enabled = false;
        _tRenderer.enabled = false;
    }
}
