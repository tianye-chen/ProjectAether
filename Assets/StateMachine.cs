using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private List<State> states;
    private State currentState;

    public StateMachine()
    {
        states = new List<State>();
        currentState = null;
    }

    public void AddState(State state)
    {
        states.Add(state);
    }

    public void RemoveState(State state)
    {
        if (currentState == state)
        {
            currentState.Exit();
            currentState = null;
        }
        states.Remove(state);
    }

    public void SetState(string name)
    {
        State newState = states.Find(s => s.Name == name);
        if (newState != null)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }
            currentState = newState;
            currentState.Enter();
        }
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}