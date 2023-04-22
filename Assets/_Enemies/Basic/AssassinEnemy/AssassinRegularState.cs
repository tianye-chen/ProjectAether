using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinRegularState : AssassinStateController
{
  public override void EnterState(AssassinEnemyController assassin)
  {
    Debug.Log("Assassin entering regular state");
  }

  public override void Update(AssassinEnemyController assassin)
  {
    // if the player is within the aggro range, enter the stealth state
    if (Vector2.Distance(assassin.transform.position, assassin.Player.transform.position) < assassin.aggroRange)
    {
      assassin.SwitchState(assassin.stealthState);
    }
  }
}

