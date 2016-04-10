using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Goal : MonoBehaviour
{
    public int _iPlayerRestriction;
    public ParticleSystem _tSystem;

    void OnTriggerEnter( Collider tCollider )
    {
        if( tCollider.tag == "Ball" )
        {
            Destroy( tCollider.gameObject, .5f );
            _tSystem.Play();

            StartCoroutine( PlayerManager.Instance._tPlayers[ tCollider.GetComponent<Ball>()._ePlayer ].ControllerVibration( 0.8f, 0.1f ) );

            GetComponent<AudioSource>().Play();
        }
    }
}
