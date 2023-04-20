using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
  // states is used to keep track of all the states that are currently applied
  public List<State> states;
  // existingStates is used to prevent duplicate states being applied
  public List<string> existingStates;
  // the character that owns this state machine
  public CharacterBase character;

  // Starts the state machine
  public StateMachine(CharacterBase characterBase)
  {
    states = new List<State>();
    existingStates = new List<string>();
    this.character = characterBase;
  }

  // Adds a state to the state machine
  public void AddState(State state)
  {
    if (!existingStates.Contains(state.stateName))
    {
      states.Add(state);
      existingStates.Add(state.stateName);
      state.SetStateMachine(this);
      state.Enter();
    }
  }

  // Removes a state from the state machine
  public void RemoveState(State state)
  {
    states.Remove(state);
    existingStates.Remove(state.stateName);
  }

  // Applies the effects of the states to the character
  public void UpdateStates()
  {
    foreach (State state in states.ToArray())
    {
      state.Update();
    }
  }
}