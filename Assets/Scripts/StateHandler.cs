using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this is an interface to represent a state which has has a transition to another state represented by
/// an Enum. by being an interface it requires a concrete implemention by the Screenhandler for example but
/// this provides the added flexibility that any state machine can use an StateHandler
/// </summary>
public interface IStateHandler
{
    string Name { get; }
    void OnEnter<T>(T transition) where T : Enum;
    void OnExit<T>(T transition) where T : Enum;
}
public abstract class StateHandler : MonoBehaviour, IStateHandler
{
  public virtual  string Name { get => (GetType().Name); }
  
  public abstract void OnEnter<T>(T transition) where T : Enum;
  
  public abstract void OnExit<T>(T transition) where T : Enum;
}
