using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndLevel : MonoBehaviour
{
    public Text _tMainText;

    public static PlayerColor _eWinner;
    public static int _iTurnCount = 0;

	void Start()
    {
        _tMainText.text = string.Format( "Player {0} win the game in {1} turn !", _eWinner.ToString(), _iTurnCount );
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
