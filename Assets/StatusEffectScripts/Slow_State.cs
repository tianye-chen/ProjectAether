using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This state slows the movement speed of the target character
public class Slow_State : State
{
  private float timer;
  private float duration;
  private float amplitude;

  // stateName as required by the State class
  public override string stateName { get => "Slow" ;}

  // Target character is slowed by the duration and the amplitude
  // Lower amplitude means slower movement
  public Slow_State(float duration, float amplitude)
  {
    timer = 0;
    this.duration = duration;
    this.amplitude = amplitude;
  }

  public override void Enter()
  {
    stateMachine.character.speed *= amplitude;
  }

  public override void Update()
  {
    if (timer < duration)
    {
      timer += Time.deltaTime;
    }  
    else
    {
      timer = 0;
      stateMachine.character.speed = stateMachine.character.maxSpeed;
      Exit();
    }
  }
}
