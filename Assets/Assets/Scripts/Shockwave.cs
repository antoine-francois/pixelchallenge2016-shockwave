using UnityEngine;
using System.Collections;

public class Shockwave : MonoBehaviour
{
    public MeshRenderer _ShockIntersect;
    public MeshRenderer _ShockDistort;
    public MeshRenderer _WaveDistort;
    private float _fLifetime = 2f;
    private float _fStartLifeTime;
    public float _fPower = 1f;
    public float _fRadius;

    void Start()
    {
        Collider[] tHits = Physics.OverlapSphere( transform.position, GetComponent<SphereCollider>().radius * _fRadius );

        for( int i = 0; i < tHits.Length; i++ )
        {
            if( tHits[i].tag == "Ball" )
            {
                Vector3 tDir = ( tHits[i].transform.position - transform.position ).normalized;
                tHits[i].GetComponent<Rigidbody>().velocity = new Vector3( tDir.x, 0f, tDir.z ) * _fPower;
            }
        }

        _fStartLifeTime = _fLifetime;

        StartCoroutine(ScaleUp(0.3f));

        Destroy( GetComponent<SphereCollider>() );
    }

	void Update()
    {
        _fLifetime -= Time.deltaTime;

        if( _fLifetime < 0f )
        {
            Destroy( gameObject );
        }
	}

    IEnumerator ScaleUp( float f )
    {
        float t = 0.0f;

        while( t < f )
        {
            t += Time.deltaTime;

            float ratio = Mathf.Clamp01(t / f);

            transform.localScale = Vector3.one * ratio * _fRadius;

            Color col = _ShockIntersect.material.GetColor( "_Color" );
            col.a = 1-ratio;

            _ShockIntersect.material.SetColor("_Color", col);
            _ShockDistort.material.SetFloat("_Displace", 1 - ratio);
            _WaveDistort.material.SetFloat("_BumpAmt", (1 - ratio)*70.0f);

            yield return null;
        }
    }
}
