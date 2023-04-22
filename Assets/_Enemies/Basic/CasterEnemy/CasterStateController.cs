using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CasterStateController
{
  public abstract void EnterState(CasterEnemyController caster);
  public abstract void Update(CasterEnemyController caster);
}
