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
                Time.timeScale = 1f;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currState = GameState.Pause;
                }
                break;
            case GameState.Pause:
                Time.timeScale = 0f;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
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