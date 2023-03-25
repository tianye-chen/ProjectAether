using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinAttackState : AssassinStateController
{
  private Vector3 assassinPosition;
  private float maxTime = 0f;
  private float elapsedTime = 0f;
  private bool isAttacking = false;

  public override void EnterState(AssassinEnemyController assassin)
  {
    Debug.Log("Assassin entering attack state");
    assassinPosition = assassin.transform.position;
  }

  public override void Update(AssassinEnemyController assassin)
  {
    assassin.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    assassin.speed = assassin.maxSpeed;

    if (!isAttacking)
    {
      assassin.StartCoroutine(Attack(assassin));
    }

  }

  // dash through the player
  IEnumerator Attack(AssassinEnemyController assassin)
  {
    isAttacking = true;
    yield return new WaitForSeconds(0.5f);

    assassin.transform.rotation = Quaternion.LookRotation(Vector3.forward, assassin.Player.transform.position - assassin.transform.position);

    maxTime = 0.125f;
    elapsedTime = 0f;

    while (elapsedTime < maxTime)
    {
      assassin.transform.Translate(Vector2.up * assassin.speed * 7.5f * Time.deltaTime);

      // keeping the sprite upright
      assassin.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
      elapsedTime += Time.deltaTime;
      yield return null;
    }

    isAttacking = false;

    // reset the rotations
    assassin.transform.eulerAngles = new Vector3(0, 0, 0);
    assassin.transform.GetChild(0).gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    yield return new WaitForSeconds(0.5f);
    assassin.SwitchState(assassin.stealthState);
  }
}
