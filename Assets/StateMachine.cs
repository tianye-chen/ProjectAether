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

public abstract class State
{
    public string Name { get; private set; }

    public State(string name)
    {
        Name = name;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public abstract void Update();
}