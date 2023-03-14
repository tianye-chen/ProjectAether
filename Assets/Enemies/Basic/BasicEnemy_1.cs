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
    private bool isRecoveringFromAttack = false;

    public override void FixedUpdate()
    {
        // check if player is within 10 units
        if (Vector2.Distance(transform.position, Player.transform.position) < aggroRange)
        {
            // move towards player until within 3 units then attack
            if (Vector2.Distance(transform.position, Player.transform.position) > attackRange && !isAttacking)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            } else 
            {
                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }
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

        isRecoveringFromAttack = true;
        // move back to original position prior to attack
        while (transform.position != posPriorToAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, posPriorToAttack, speed * 3 * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(attackSpeed * 2);
        isRecoveringFromAttack = false;
        isAttacking = false;
    }

    // when the enemy hits the player, deal damage
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(1);
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
