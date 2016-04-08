using UnityEngine;
using System.Collections;

public class Slab : MonoBehaviour
{
    public bool _bCooldown = false;
    public int _iCooldownDuration = 2;
    

    private bool _bCooldownActive = false;
    private int _iCooldownTimer = 0;

    public void NextTurn()
    {
        if( _bCooldownActive )
        {
            _iCooldownTimer++;
            if( _iCooldownTimer > _iCooldownDuration ) {
                _bCooldownActive = false;
            }
        }
    }

    public virtual void Activate( Ball tBall )
    {
        if( _bCooldown )
        {
            _bCooldownActive = true;
            _iCooldownTimer = 0;
        }
    }

    void OnTriggerEnter( Collider tCollider )
    {
        if( tCollider.tag == "Ball" && !_bCooldownActive )
        {
            Activate( tCollider.GetComponent<Ball>() );
        }
    }
}
