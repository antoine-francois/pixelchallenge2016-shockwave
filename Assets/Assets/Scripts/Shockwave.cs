using UnityEngine;
using System.Collections;

public class Shockwave : MonoBehaviour
{
    private float _fLifetime = 2f;

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
}
