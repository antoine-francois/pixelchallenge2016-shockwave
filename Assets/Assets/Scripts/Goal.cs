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
            Ball tBall = tCollider.GetComponent<Ball>();
            int iScore = PlayerManager.Instance._tPlayers[tBall._ePlayer].IncrementScore();

            StartCoroutine( PlayerManager.Instance._tPlayers[tBall._ePlayer].ControllerVibration( 0.8f, 0.1f ) );

            Destroy( tCollider.gameObject, .5f );
            _tSystem.Play();

            if( iScore == BallManager.Instance._iNbBalls / GameSettings._iNbPlayers )
            {
                EndLevel._iTurnCount = PlayerManager.Instance.TurnCount;
                EndLevel._eWinner = tBall._ePlayer;

                SceneManager.LoadScene( "EndLevel" );
            }
        }
    }
}
