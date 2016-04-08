using UnityEngine;
using System.Collections.Generic;

public class ForceField : GenericMechanism
{
    private Collider _tCollider;
    private MeshRenderer _tRenderer;

    public List<Color> _tForceFieldColor = new List<Color>();

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

        gameObject.layer = LayerMask.NameToLayer( "P" + ( tBall._iPlayer + 1 ) );
        GetComponentInChildren<MeshRenderer>().materials[0].color = _tForceFieldColor[tBall._iPlayer];
    }

    public override void Disable()
    {
        base.Disable();

        _tCollider.enabled = false;
        _tRenderer.enabled = false;
    }
}
