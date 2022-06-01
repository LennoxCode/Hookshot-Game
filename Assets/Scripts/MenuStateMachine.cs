using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;
/// <summary>
/// this is the final implementation of the statemachine presented in the lesson slides.
/// it has a state for every part of the GUI which are trigger by buttons to from the main menu state to
/// the level select state for example. In addition some transitions trigger based on events like
/// the win state which cant be triggered by a button(maybe for the casuals in the future) I used an
/// anonymous function which is subscribed to the event to trigger this event
/// </summary>
public class MenuStateMachine : StateMachine<MenuTransitions>
{
    public static MenuStateMachine instance;
    [field: SerializeField] public StateHandler MainMenuHandler { get; private set; }
    [field: SerializeField] public StateHandler OptionHandler { get; private set; }
    [field: SerializeField] public StateHandler LevelsHandler { get; private set; }
    [field: SerializeField] public StateHandler HUDHandler { get; private set; }
    [field: SerializeField] public StateHandler PauseHandler { get; private set; }
    [field: SerializeField] public StateHandler LostHandler { get; private set; }
    [field: SerializeField] public StateHandler WinHandler { get; private set; }
    [field: SerializeField] public StateHandler LeaderBoardHandler { get; private set; }
    private void Awake()
    {
        if(instance != null){Destroy(gameObject);}
        instance = this;
        DontDestroyOnLoad(this);
        AddTransition(MainMenuHandler, OptionHandler, MenuTransitions.OptionSelected);
        AddTransition(OptionHandler, MainMenuHandler, MenuTransitions.MainMenuSelected);
        
        AddTransition(MainMenuHandler, LevelsHandler, MenuTransitions.LevelMenuSelected);
        AddTransition(LevelsHandler, MainMenuHandler, MenuTransitions.MainMenuSelected);
        
        AddTransition(LevelsHandler, HUDHandler, MenuTransitions.GameActive);
        
        AddTransition(HUDHandler, PauseHandler, MenuTransitions.Pause);
        AddTransition(PauseHandler, HUDHandler, MenuTransitions.ResumeGame);
        AddTransition(HUDHandler, LostHandler, MenuTransitions.GameLost);
        AddTransition(PauseHandler, MainMenuHandler, MenuTransitions.MainMenuSelected);
        AddTransition(HUDHandler, WinHandler, MenuTransitions.GameWon);
        AddTransition(WinHandler, HUDHandler, MenuTransitions.ResumeGame);
        AddTransition(WinHandler, LeaderBoardHandler, MenuTransitions.LeaderBoardSelected);
        AddTransition(LeaderBoardHandler, WinHandler, MenuTransitions.ShowVictoryScreen);
      
    }

    void Start()
    {
        base.Start();
        Goal.playerEnteredGoal += () => Trigger(MenuTransitions.GameWon);
        SceneController.instance.OnLevelLoaded += () => Trigger(MenuTransitions.ResumeGame);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Trigger(MenuTransitions.Pause);
    }
}

public enum MenuTransitions
{
    MainMenuSelected,
    OptionSelected,
    LevelMenuSelected,
    LeaderBoardSelected,
    GameLost,
    ShowVictoryScreen,
    GameWon,
    Pause,
    GameActive,
    ResumeGame
}