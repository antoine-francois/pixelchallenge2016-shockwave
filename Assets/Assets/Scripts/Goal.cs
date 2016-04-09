using UnityEngine;
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
            PlayerManager.Instance._tPlayers[tBall._iPlayer]._iScore++;
            Destroy( tCollider.gameObject, .5f );
            MenuGameState.Instance._tMenuManager.UpdateHUD();
            _tSystem.Play();
        }
    }
}
