using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
  // public variables
  public float aggroRange;
  public float disengageRange;
  public float attackRange;
  public float attackSpeed;
  public bool playerInAttackRange;
  public bool inDisengageRange;


  // assets
  public GameObject Player;

  // private variables

  // Start is called before the first frame update
  public override void Start()
  {
    base.Start();

    if (Player == null)
      Player = GameObject.FindGameObjectWithTag("Player");

    // prevent all collisions between layer zero GameObjects
    Physics2D.IgnoreLayerCollision(0 , 0);

    health = maxHealth;
  }

  public virtual void move()
  {
    // check if player is within disengageRange
    if (Vector2.Distance(transform.position, Player.transform.position) < disengageRange)
    {
      inDisengageRange = true;
    }
    else
    {
      inDisengageRange = false;
    }

    // check if player is within aggroRange
    if (Vector2.Distance(transform.position, Player.transform.position) < aggroRange || inDisengageRange)
    {
      // move towards player until within attackRange
      if (Vector2.Distance(transform.position, Player.transform.position) > attackRange)
      {
        playerInAttackRange = false;
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
      }
      else
      {
        playerInAttackRange = true;
      }
    }
  }

  public void facePlayer(){
    // turn to face the player
    if (Player.transform.position.x < transform.position.x)
    {
      transform.localScale = new Vector2(-1, 1);
      direction = 1;
    }
    else
    {
      transform.localScale = new Vector2(1, 1);
      direction = 2;
    }
  }

  public void LookAt2D(GameObject self, GameObject target)
  {
    self.transform.up = target.transform.position - self.transform.position;
  }
}
