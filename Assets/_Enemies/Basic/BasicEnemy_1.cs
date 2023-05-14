using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Basic Enemy 1
 * 
 *  Basic melee enemy
 *  When the player enters within 10 units, the enemy will chase the player until within 3 units
 *  When within 3 units, the enemy will perform a lunge attack
 *  After the attack, the enemy will move back to its original position prior to the attack
 * 
 *  default common values:
 *      maxHealth = 10
 *      maxSpeed = 2
 *      maxAtk = 3
 *      maxDef = 3
 *      aggroRange = 10
 *      disengageRange = 10
 *      attackRange = 3
 *      attackSpeed = 1.5
 *      xp = 10
 */
public class BasicEnemy_1 : EnemyBase
{
  // private variables
  private float attackTimer = 0.0f;
  private Vector3 originalPosition;


  public override void FixedUpdate()
  {
    base.FixedUpdate();

    // check if player is within 10 units
    if (isPlayerInAttackRange() && attackTimer > attackSpeed)
    {
      originalPosition = transform.position;
      attackTimer = 0;
      StartCoroutine(Attack());
    }
    else
    {
      move();
      attackTimer += Time.deltaTime;
    }
  }

  private IEnumerator Attack()
  {
    Debug.Log("Attacking");
    // lunge attack
    while (Vector2.Distance(transform.position, Player.transform.position) > 0.1)
    {
      transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime * 3);
      this.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
      yield return null;
    }

    // move backwards
    while (Vector2.Distance(transform.position, Player.transform.position) < attackRange)
    {
      transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, -speed * Time.deltaTime * 3);
      this.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
      yield return null;
    }
  }

  // when the enemy hits the player, deal damage
  public void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      collision.gameObject.GetComponent<PlayerController>().TakeDamage(atk);
    }
  }
}
