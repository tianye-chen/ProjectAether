using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
  protected StateMachine stateMachine;
  public abstract string stateName { get; }

  public virtual void Exit()
  {
    stateMachine.RemoveState(this);
  }

  public virtual void SetStateMachine(StateMachine stateMachine)
  {
    this.stateMachine = stateMachine;
  }

  public virtual void Enter() { }

  public abstract void Update();
}
