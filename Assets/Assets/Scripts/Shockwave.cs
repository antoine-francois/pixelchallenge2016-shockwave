using UnityEngine;
using System.Collections;

public class Shockwave : MonoBehaviour
{
    private float _fLifetime = 2f;
    public float _fPower = 1f;

    void Start()
    {
        Collider[] tHits = Physics.OverlapSphere( transform.position, GetComponent<SphereCollider>().radius * transform.localScale.x );

        for( int i = 0; i < tHits.Length; i++ )
        {
            if( tHits[i].tag == "Ball" )
            {
                Vector3 tDir = ( tHits[i].transform.position - transform.position ).normalized;
                tHits[i].GetComponent<Rigidbody>().velocity = new Vector3( tDir.x, 0f, tDir.z ) * _fPower;
            }
        }

        Destroy( GetComponent<SphereCollider>() );
    }

	void Update()
    {
        _fLifetime -= Time.deltaTime;
        if( _fLifetime < 0f ) {
            Destroy( gameObject );
        }
	}
}
