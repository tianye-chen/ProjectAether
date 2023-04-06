using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
 * Basic Enemy 2
 * 
 *  Basic ranged enemy
 *  When the player enters within 10 units, the enemy will chase the player until within 5 units
 *  When within 5 units, the enemy will circle around the player
 *  And attack the player with projectiles
 *  When within 4 units, the enemy will move away from the player
 *
 *  default common values:
 *      maxHealth = 10
 *      maxSpeed = 1.5
 *      maxAtk = 1
 *      maxDef = 3
 *      aggroRange = 10
 *      disengageRange = 10
 *      attackRange = 5
 *      attackSpeed = 0.5 
*/
public class BasicEnemy_2 : EnemyBase
{
  // public variables
  public GameObject ProjectileObject;
  public int numProjectiles = 1;

  // private variables
  private float currHealth;
  private float attackTimer = 0;

  public override void FixedUpdate()
  {
    base.FixedUpdate();
    move();

    // if the player is within 4 units, move away from the player
    if (Vector2.Distance(transform.position, Player.transform.position) < attackRange - 1)
    {
      transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, -speed * Time.deltaTime);
      Attack();

    }
    else if (playerInAttackRange)
    {
      // circle around the player
      transform.RotateAround(Player.transform.position, Vector3.forward, 20 * Time.deltaTime);

      // prevent enemy from rotating upside down
      transform.eulerAngles = new Vector3(0, 0, 0);

      Attack();
    }
  }

  void Attack()
  {
    // fire projectile based on attack speed
    if (attackTimer > attackSpeed)
    {
      for (int i = 0; i < 30 * numProjectiles; i += 30)
      {
        // Instantiate projectile
        GameObject projectile = Instantiate(ProjectileObject, transform.position, Quaternion.identity);

        LookAt2D(projectile.transform, Player.transform);

        // rotate projectile by i degrees and adjust spread based on number of projectiles
        projectile.transform.Rotate(0, 0, i - 15 * (numProjectiles - 1));
      }
      attackTimer = 0;
    }
    else
    {
      attackTimer += Time.deltaTime;
    }
  }
}
