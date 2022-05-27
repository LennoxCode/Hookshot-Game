using System;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    public static GameStateMachine instance;
    public GameState currState { private set; get; }

    private void Awake()
    {
        if(instance != null){Destroy(gameObject);}
        instance = this;
        currState = GameState.InMenu;
        DontDestroyOnLoad(this);
        

    }

    private void Start()
    {
        SceneController.instance.OnLevelLoaded += () => currState = GameState.Active;
    }

    private void Update()
    {
        switch (currState)
        {
            case GameState.Active:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Time.timeScale = 0f;
                    currState = GameState.Pause;
                }
                break;
            case GameState.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Time.timeScale = 1f;
                    currState = GameState.Active;
                }

                break;
        }
    }
    
}

public enum GameState
{
    Pause,
    Active,
    InMenu,
}