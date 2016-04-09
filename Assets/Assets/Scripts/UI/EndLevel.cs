using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndLevel : MonoBehaviour
{
    public Text _tMainText;

    public static int _iWinner = -1;
    public static int _iTurnCount = 0;

	void Start()
    {
        _tMainText.text = string.Format( "Player {0} win the game in {1} turn !", _iWinner + 1, _iTurnCount );
	}

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene( "MapScene" );
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene( "MenuScene" );
    }
}
