using UnityEngine;
using System.Collections;

public class ShockwaveFactory : MonoBehaviour
{
    public static ShockwaveFactory Instance {get; private set; }

    public GameObject _tShockwavePrefab;
    public AudioSource _tShock;
    public GameObject _tShockwavePreviewPrefab;
    public AudioSource _tCharge;

    public Transform _tTarget;

	void Awake()
    {
        Instance = this;
	}


    public void CreateShockwave( float fPower, float fRadius )
    {
        GameObject tWave = Instantiate(_tShockwavePrefab);
        tWave.transform.localPosition = new Vector3(_tTarget.position.x, _tTarget.position.y, _tTarget.position.z);
        tWave.GetComponent<Shockwave>()._fRadius = fRadius;

        tWave.GetComponent<Shockwave>()._fPower = fPower;

        if (_tCharge.isPlaying)
            _tCharge.Stop();

        _tShock.Play();
    }

    public GameObject CreateShockwavePreview( float fRadius )
    {
        GameObject tWave = Instantiate(_tShockwavePreviewPrefab);
        tWave.transform.localPosition = new Vector3(_tTarget.position.x, _tTarget.position.y, _tTarget.position.z);
        tWave.transform.localScale = new Vector3(fRadius, fRadius, fRadius);
        _tCharge.Play();

        return tWave;
    }
}
