using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterCastingState : CasterStateController
{
  private GameObject Inst;
  private float timer;

  public override void EnterState(CasterEnemyController caster)
  {
    Debug.Log("Caster entering casting state");

    // spawn numCast amount of areaMarkers at the player location which will spawn the a beam attack
    // the spawn location of the areaMarkers is offset by half the number of casts so that the beams will be centered on the player
    for (int i = (int)Math.Ceiling(0 - (double)caster.numCasts / 2); i < Math.Ceiling((double)caster.numCasts / 2); i++)
    {
      Inst = MonoBehaviour.Instantiate(caster.areaMarker, new Vector2(caster.Player.transform.position.x + caster.castRadius / 2 * i, caster.Player.transform.position.y), Quaternion.identity);
      Inst.GetComponent<areaMarker>().InstObject = caster.castingObject;
      Inst.GetComponent<areaMarker>().radius = caster.castRadius;
      Inst.GetComponent<areaMarker>().duration = caster.castTime;
    }
  }

  public override void Update(CasterEnemyController caster)
  {
    // channel for the cast time
    timer += Time.deltaTime;
    if (timer > caster.castTime)
    {
      caster.SwitchState(caster.regularState);
      timer = 0;
    }
  }
}
