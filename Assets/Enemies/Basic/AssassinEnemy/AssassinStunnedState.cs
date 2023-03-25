using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinStunnedState : AssassinStateController
{

  private float timer = 0;

  public override void EnterState(AssassinEnemyController assassin)
  {
    Debug.Log("Assassin entering stunned state");
  }

  public override void Update(AssassinEnemyController assassin)
  {
    // stun for the stun time
    timer += Time.deltaTime;
    if (timer > assassin.stunTime)
    {
      assassin.SwitchState(assassin.regularState);
      timer = 0;
    }
  }
}
