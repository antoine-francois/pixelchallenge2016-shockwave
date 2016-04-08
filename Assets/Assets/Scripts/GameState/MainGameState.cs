using UnityEngine;
using System.Collections;

public class MainGameState : MonoBehaviour
{
    static public MainGameState Instance { get; private set; }

    public StateManager _tManager { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

	void Start ()
    {
        _tManager = new StateManager();
        _tManager.Start(new SplashScreen());
	}
	
	void Update ()
    {
        _tManager.Update();
	}
}
