using UnityEngine;
using System;
using System.Collections;

public class PhysicUpdateTest : MonoBehaviour
{
    public GameObject _tShockwavePrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        if( Input.GetKeyDown( KeyCode.Mouse0 ) )
        {
            Vector3 tMousePos = Camera.main.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.localPosition.y ) );

            RaycastHit tHit;
            if( Physics.Raycast( tMousePos, Vector3.down, out tHit ) )
            {
                Debug.Log( tMousePos );

                GameObject tWave = Instantiate( _tShockwavePrefab );
                //tWave.transform.localScale = new Vector3( 0, 0, 0 );
                tWave.transform.localPosition = new Vector3( tMousePos.x, 0f, tMousePos.z );

                BallManager.Instance.DoStuffOnEachBalls( new BallActionCallback( c => {
                    c._tRigidbody.WakeUp();
                } ) );
            }

        }
	}
}
