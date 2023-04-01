using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainEnemy : EnemyBase
{
  // public variables
  public GameObject projectile;
  public float numShots;
  public float amplitude;


  // private variables
  private GameObject instProj;
  private float attackTimer = 0.0f;

  public override void FixedUpdate()
  {
    move();
    facePlayer();

    if (playerInAttackRange)
    {
      Attack();
    }
  }

  private void Attack()
  {
    if (attackTimer > attackSpeed)
    {
      // fire a cluster of projectiles based on numShots in a circle towards the player
        for (int i = 0; i < 360; i += 360 / (int)numShots)
        {
            Vector3 circleVector = new Vector3(Mathf.Cos(i * Mathf.Deg2Rad) * amplitude, Mathf.Sin(i * Mathf.Deg2Rad) * amplitude);
            instProj = Instantiate(projectile, circleVector + transform.position, Quaternion.identity);
            LookAt2D(instProj.transform, Player.transform, circleVector);
        }

        attackTimer = 0;
    }
    else
    {
      attackTimer += Time.deltaTime;
    }
  }
}
