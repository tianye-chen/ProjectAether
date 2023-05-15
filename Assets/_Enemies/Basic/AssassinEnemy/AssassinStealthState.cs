using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinStealthState : AssassinStateController
{
  private float stealthTimer = 0f;

  public override void EnterState(AssassinEnemyController assassin)
  {
    Debug.Log("Assassin entering stealth state");
  }

  public override void Update(AssassinEnemyController assassin)
  {
    assassin.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0f);
    assassin.speed = Random.Range(15f, 25f);

    assassin.move();

    // rotate around the player
    if (stealthTimer < assassin.stealthTime)
    {
      assassin.transform.RotateAround(assassin.Player.transform.position, Vector3.forward, assassin.speed);
      assassin.transform.eulerAngles = new Vector3(0, 0, 0);
      stealthTimer += Time.deltaTime;
    }
    else
    {
      assassin.SwitchState(assassin.attackState);
      stealthTimer = 0f;
    }
  }
}
