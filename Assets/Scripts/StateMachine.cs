using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public abstract class StateMachine<Transition> : MonoBehaviour where Transition : struct, Enum
{
    [SerializeField] private List<TransitionConnection<Transition>> transitions;
    [field: SerializeField] public StateHandler currentState { get; private set; }
    private Dictionary<StateHandler, List<TransitionConnection<Transition>> stateToTransitions;

    protected void AddTransition(StateHandler from, StateHandler to, Transition transition)
    {
        if (transitions.Any(x => x.from == from && x.transition.Equals(transition)))
        {
            Debug.LogError($"There is already a transition from {from.Name} with transition {transition}");
            return;
        }
        transitions.Add(new TransitionConnection from, to, transition));
    }

    protected void Commit(StateHandler optionalEntry = null)
    {
        if (stateToTransitions != null)
        {
            Debug.LogError("cannot commit state machine is already built");
            return;
        }

        stateToTransitions = new Dictionary<StateHandler, List<TransitionConnection<Transition>>>();
        foreach (var transition in transitions)
        {
            if (stateToTransitions.ContainsKey(transition.from))
            {
                stateToTransitions[transition.from].Add(transition);
            }
            else
            {
                stateToTransitions[transition.from] = new List<TransitionConnection>();
            }
        }

        if (optionalEntry != null)
        {
            currentState = optionalEntry;
            //currentState.OnEnter();
        }
        else if (transitions.Count > 0)
        {
            currentState = transitions[0].from;
        }
    }
    public bool Trigger(Transition transition)
    {

        var possibleTransitons = stateToTransitions[currentState];
        var concreteTransition = possibleTransitons.FirstOrDefault(x => x.transition.Equals(transition));
        if (concreteTransition == null) return false;
        concreteTransition.from.OnExit(transition);
        currentState = concreteTransition.to;
        concreteTransition.to.OnEnter(transition);
        return true;
    }
}
