using UnityEngine;
using System.Collections;

public class Shockwave : MonoBehaviour
{
    private float _fLifetime = 2f;

    private const float WAVE_FORCE = 5f;

    void Start()
    {
        //Destroy( GetComponent<SphereCollider>() );
    }

	void Update()
    {
        _fLifetime -= Time.deltaTime;
        if( _fLifetime < 0f ) {
            Destroy( gameObject );
        }
	}

    void OnTriggerEnter( Collider tCollider )
    {
        if( tCollider.tag == "Ball" )
        {
            Vector3 tDir = tCollider.transform.position - transform.position;
            tCollider.GetComponent<Rigidbody>().velocity = new Vector3( tDir.x, 0f, tDir.z ) * WAVE_FORCE;
        }
    }
}
