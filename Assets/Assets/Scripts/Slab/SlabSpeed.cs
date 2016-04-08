using UnityEngine;
using System.Collections;

public class SlabSpeed : Slab
{
    [Range(0.2f, 5f)]
    public float _fMultiplicator = 2f;

    public override void Activate( Ball tBall )
    {
        base.Activate( tBall );
        tBall._tRigidbody.velocity *= _fMultiplicator;
    }
}
