using UnityEngine;
using System.Collections;

public class ForceField : GenericMechanism
{

	// Use this for initialization
	public override void Start()
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}

    public override void Activate( Ball tBall )
    {
        base.Activate( tBall );

        GetComponentInChildren<MeshRenderer>().materials[0].color = Color.red;
    }

    public override void Disable()
    {
        base.Disable();

        GetComponentInChildren<MeshRenderer>().materials[0].color = Color.white;
    }
}
