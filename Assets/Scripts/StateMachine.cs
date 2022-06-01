using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
/// <summary>
/// this class was taken from the slides
/// this class for the StateMachine is abstract so it cant be instantiated, so a concrete stateMachine implementation
/// class muss inherit from it. However this provides base functionality like triggering a transition( the fact
/// that a state handler is an interface comes in handy again). the functionality of changing through states
/// is mainly realized by a dictionary which uses a statehandler as a key and provides a list of viable transitions
/// from this state
/// </summary>
public abstract class StateMachine<Transition> : MonoBehaviour where Transition : struct, Enum
{
    [SerializeField] private List<TransitionConnection<Transition>> transitions;
    [field: SerializeField] public StateHandler currentState { get; private set; }
    private Dictionary<StateHandler, List<TransitionConnection<Transition>>> _stateToTransitions;

    protected void AddTransition(StateHandler from, StateHandler to, Transition transition)
    {
        if (transitions.Any(x => x.from == from && x.transition.Equals(transition)))
        {
            Debug.LogError($"There is already a transition from {from.Name} with transition {transition}");
            return;
        }
        transitions.Add(new TransitionConnection<Transition>(from, to, transition));
    }

    protected void Commit(StateHandler optionalEntry = null)
    {
        if (_stateToTransitions != null)
        {
            Debug.LogError("cannot commit state machine is already built");
            return;
        }

        _stateToTransitions = new Dictionary<StateHandler, List<TransitionConnection<Transition>>>();
        foreach (var transition in transitions)
        {
            if (_stateToTransitions.ContainsKey(transition.from))
            {
                _stateToTransitions[transition.from].Add(transition);
            }
            else
            {
                _stateToTransitions[transition.from] = new List<TransitionConnection<Transition>>() {transition};
            }
        }

        if (optionalEntry != null)
        {
            currentState = optionalEntry;
            //currentState.OnEnter(RuntimeTransition.fromInit);
        }
        else if (transitions.Count > 0)
        {
            currentState = transitions[0].from;
        }
    }
    public bool Trigger(Transition transition)
    {
        Debug.Log(currentState);
        var possibleTransitons = _stateToTransitions[currentState];
       // Debug.Log(possibleTransitons.Count);
        var concreteTransition = possibleTransitons.FirstOrDefault(x => x.transition.Equals(transition));
        if (concreteTransition == null) return false;
        concreteTransition.from.OnExit(transition);
        currentState = concreteTransition.to;
        concreteTransition.to.OnEnter(transition);
        return true;
    }

    protected void Start()
    {
        if (_stateToTransitions != null) return;
        Commit();
    }

    public void Trigger(string trigger)
    {
        if (!Enum.TryParse<Transition>(trigger, out var value))
        {
            Debug.LogError($"{trigger} is not a valid transition value");
            return;
        }

        Trigger(value);
    }
}
