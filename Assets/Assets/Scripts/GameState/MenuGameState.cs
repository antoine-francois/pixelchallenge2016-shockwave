using UnityEngine;
using System.Collections;

public class MenuGameState : MonoBehaviour
{
    static public MenuGameState Instance { get; private set; }

    public StateManager _tManager { get; private set; }
    public MenuManager _tMenuManager;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
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
