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
  private float alternateAttackCounter = 0;
  private float innerAttackCounter = 0;
  private bool enteredRandomMove = false;

  public override void FixedUpdate()
  {
    base.FixedUpdate();
    move();
    facePlayer();

    if (isPlayerInAttackRange())
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
        instProj.GetComponent<BasicProjectile>().SetColor(new Color(0, 155, 155));

        // adds the slow effect to the projectile
        instProj.GetComponent<BasicProjectile>().stateEffect = new Slow_State(2, 0.25f);

        // three different attack patterns

        // pattern 1: fire projectiles that start off as a circle and each projectile will move towards the player independently
        if (alternateAttackCounter % 3 == 0)
        {
          LookAt2D(instProj.transform, Player.transform);

          // pattern 2: fire projectiles in a circle arrangement towards the player
        } else if (alternateAttackCounter % 3 == 1)
        {
          LookAt2D(instProj.transform, Player.transform, circleVector);

          // pattern 3: a combination of pattern 1 and 2
        } else 
        {
          if (innerAttackCounter % 2 == 0)
          {
            LookAt2D(instProj.transform, Player.transform);
          } else
          {
            LookAt2D(instProj.transform, Player.transform, circleVector);
          }

          innerAttackCounter++;
        } 
      }

      alternateAttackCounter++;
      attackTimer = 0;
    }
    else
    {
      attackTimer += Time.deltaTime;
    }
  }

  public override void move()
  {
    base.move();

    if (isPlayerInAttackRange() && !enteredRandomMove)
    {
      // move randomly within 2 units
      StartCoroutine(randomMove(1));
    }
  }

  IEnumerator randomMove(float range)
  {
    enteredRandomMove = true;
    Vector2 newPos;

    float timeout = 2;
    float timeoutTimer = 0;

    while (isPlayerInAttackRange())
    {
      newPos = new Vector2(Random.Range(transform.position.x - range, transform.position.x + range), Random.Range(transform.position.y - range, transform.position.y + range));
      
      while (Vector2.Distance(transform.position, newPos) > 0.1f && timeoutTimer < timeout)
      {
        transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
        timeoutTimer += Time.deltaTime;
        yield return null;
      }
      
      timeoutTimer = 0;
      yield return new WaitForSeconds(attackSpeed);
    }

    enteredRandomMove = false;
  }
}
