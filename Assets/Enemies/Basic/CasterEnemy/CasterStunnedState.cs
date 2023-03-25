using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterStunnedState : CasterStateController
{
  private float timer = 0;

  public override void EnterState(CasterEnemyController caster)
  {
    Debug.Log("Caster entering stunned state");
  }

  public override void Update(CasterEnemyController caster)
  {
    // stun for the stun time
    timer += Time.deltaTime;
    if (timer > caster.stunTime)
    {
      caster.SwitchState(caster.regularState);
      timer = 0;
    }
  }
}
