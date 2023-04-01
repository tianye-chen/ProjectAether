using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public CharacterBase Character;

    public string Name { get; protected set; }

    public State(string name)
    {
        Name = name;
    }

    public State() { }

    public State(CharacterBase character, string name)
    {
        Character = character;
        Name = name;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public abstract void Update();
}
