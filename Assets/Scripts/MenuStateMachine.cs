using System;
using UnityEngine;
using Object = System.Object;

public class MenuStateMachine : StateMachine<MenuTransitions>
{
    [field: SerializeField] public StateHandler MainMenuHandler { get; private set; }
    [field: SerializeField] public StateHandler OptionHandler { get; private set; }
    [field: SerializeField] public StateHandler LevelsHandler { get; private set; }
    [field: SerializeField] public StateHandler HUDHandler { get; private set; }
    [field: SerializeField] public StateHandler PauseHandler { get; private set; }
    [field: SerializeField] public StateHandler LostHandler { get; private set; }
    private void Awake()
    {
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
    GameLost,
    Pause,
    GameActive,
    ResumeGame
}