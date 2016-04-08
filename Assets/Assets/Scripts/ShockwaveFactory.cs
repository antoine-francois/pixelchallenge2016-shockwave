using UnityEngine;
using System.Collections;

public class ShockwaveFactory : MonoBehaviour
{
    public static ShockwaveFactory Instance {get; private set; }

    public GameObject _tShockwavePrefab;


	void Awake()
    {
        Instance = this;
	}


    public void CreateShockwave()
    {
        Vector3 tCamPos = Camera.main.transform.position;

        RaycastHit tHit;
        if( Physics.Raycast( tCamPos, Vector3.down, out tHit ) )
        {
            GameObject tWave = Instantiate( _tShockwavePrefab );
            tWave.transform.localPosition = new Vector3( tCamPos.x, 0f, tCamPos.z );

            /*BallManager.Instance.DoStuffOnEachBalls( new BallActionCallback( c => {
                c._tRigidbody.WakeUp();
            } ) );*/
        }
    }
}
