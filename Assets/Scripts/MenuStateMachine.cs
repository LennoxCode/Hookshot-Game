using System;
using UnityEngine;

public class MenuStateMachine : StateMachine<MenuTransitions>
{
    [field: SerializeField] public StateHandler MainMenuHandler { get; private set; }
    [field: SerializeField] public StateHandler OptionHandler { get; private set; }

    private void Awake()
    {
        AddTransition(MainMenuHandler, OptionHandler, MenuTransitions.OptionSelected);
        AddTransition(OptionHandler, MainMenuHandler, MenuTransitions.MainMenuSelected);
    }
    
}

public enum MenuTransitions
{
    MainMenuSelected,
    OptionSelected,
    LevelMenuSelected,
    GameLost,
    Pause,
}