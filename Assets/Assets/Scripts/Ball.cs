using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public int _iPlayer = 0;

    public Rigidbody _tRigidbody { get; private set; }
    public ParticleSystem _tSparks;

	void Start()
    {
        BallManager.Instance.AddBall( this );
        _tRigidbody = GetComponent<Rigidbody>();
	}

	void Update()
    {
	}

    void FixedUpdate()
    {

    }

    void OnDestroy()
    {
        BallManager.Instance.RemoveBall( this );
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            GameObject tNewPS = Instantiate<GameObject>( _tSparks.gameObject );
            tNewPS.transform.position = contact.point;
            tNewPS.GetComponent<ParticleSystem>().Play();
            tNewPS.transform.localScale = Vector3.one * Mathf.Clamp01(_tRigidbody.velocity.magnitude/10.0f);
            Destroy(tNewPS, 5.0f);
        }
    }
}
