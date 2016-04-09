using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public AudioSource[] _Hits;
    public AudioSource[] _Walls;
    public float _fRollSoundLimit;

    public PlayerColor _ePlayer;

    public Rigidbody _tRigidbody { get; private set; }
    public AudioSource _tRoll { get; private set; }
    public ParticleSystem _tSparks;

	void Start()
    {
        BallManager.Instance.AddBall( this );
        _tRigidbody = GetComponent<Rigidbody>();
        _tRoll = GetComponent<AudioSource>();
        _tRoll.pitch = Random.Range(.7f, 1.3f);
	}

	void Update()
    {
        if (transform.position.y < _fRollSoundLimit)
        {
            _tRoll.volume = _tRigidbody.velocity.magnitude / 10.0f;
            Debug.Log( _tRigidbody.velocity.magnitude / 10.0f );
        }
        else
            _tRoll.volume = 0.0f;
	}

    void FixedUpdate()
    {

    }

    void OnDestroy()
    {
        BallManager.Instance.RemoveBall( this );
    }

    void OnCollisionEnter( Collision collision )
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            GameObject tNewPS = Instantiate<GameObject>( _tSparks.gameObject );
            tNewPS.transform.position = contact.point;
            tNewPS.GetComponent<ParticleSystem>().Play();
            tNewPS.transform.localScale = Vector3.one * Mathf.Clamp01(_tRigidbody.velocity.magnitude/10.0f);
            Destroy(tNewPS, 5.0f);

            if( contact.otherCollider.tag == "Ball" )
            {
                AudioSource tRdmSource = _Hits[Random.Range(0, _Hits.Length)];
                tRdmSource.pitch = Random.Range(0.7f, 1.3f);
                tRdmSource.volume = Mathf.Clamp01(_tRigidbody.velocity.magnitude / 10.0f);
                tRdmSource.Play();
            }
            else if (contact.otherCollider.tag == "Wall")
            {
                AudioSource tRdmSource = _Walls[Random.Range(0, _Walls.Length)];
                tRdmSource.pitch = Random.Range(0.7f, 1.3f);
                tRdmSource.volume = Mathf.Clamp01(_tRigidbody.velocity.magnitude / 10.0f);
                tRdmSource.Play();
            }
        }
    }
}
