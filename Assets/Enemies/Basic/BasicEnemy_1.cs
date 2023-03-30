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
 *      speed = 2
 *      aggroRange = 10
 *      disengageRange = 10
 *      attackRange = 3
 *      attackSpeed = 0.75
 */
public class BasicEnemy_1 : EnemyBase
{
  // private variables
  private bool isAttacking = false;

  public override void FixedUpdate()
  {
    move();

    // check if player is within 10 units
    if (!isAttacking && playerInAttackRange)
    {
      StartCoroutine(Attack());
    }
  }

  IEnumerator Attack()
  {
    isAttacking = true;
    Vector3 posPriorToAttack = transform.position;
    Vector3 playerPos = Player.transform.position;

    yield return new WaitForSeconds(attackSpeed);

    // move to player position until within 1 unit
    while (Vector2.Distance(transform.position, playerPos) > 1f)
    {
      transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * 3 * Time.deltaTime);
      yield return null;
    }

    // move back to original position prior to attack
    while (transform.position != posPriorToAttack)
    {
      transform.position = Vector2.MoveTowards(transform.position, posPriorToAttack, speed * 3 * Time.deltaTime);
      yield return null;
    }

    yield return new WaitForSeconds(attackSpeed * 2);
    isAttacking = false;
  }

  // when the enemy hits the player, deal damage
  public void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      collision.gameObject.GetComponent<PlayerController>().TakeDamage(1);
    }
  }
}
