using UnityEngine;

public class GameStateMachine : StateMachine<GameStateTransition>
{
    public static GameStateMachine instance;
    [field: SerializeField] public StateHandler PauseStateHandler { get; private set; }
    [field: SerializeField] public StateHandler ActiveGameHandler { get; private set; }
}

public enum GameStateTransition
{
    Pause,
    Loose,
    Active
}