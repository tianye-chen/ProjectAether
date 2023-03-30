using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AssassinStateController
{
  public abstract void EnterState(AssassinEnemyController assassin);
  public abstract void Update(AssassinEnemyController assassin);
}
