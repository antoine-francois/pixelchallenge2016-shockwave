using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndLevel : MonoBehaviour
{
	void Start()
    {
	
	}

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene( "MapScene" );
    }
}
