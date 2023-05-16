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
  public float xp;
  public bool inDisengageRange;


  // assets
  public GameObject Player;

  // private variables

  // Start is called before the first frame update
  public override void Start()
  {
    base.Start();

    if (Player == null)
    {
      Player = GameObject.FindGameObjectWithTag("Player");
    }

    Physics2D.IgnoreLayerCollision(0 , 8);
    Physics2D.IgnoreLayerCollision(0 , 0);
    health = maxHealth;

    atk += (atk * (FloorLevelManager.floorLevel * 0.05f));
    health += (health * (FloorLevelManager.floorLevel * 0.05f));
  }

  public virtual void move()
  {
    // check if player is within aggroRange
    if (isPlayerInAggroRange() || isPlayerInDisengageRange())
    {
      // move towards player until within attackRange
      if (!isPlayerInAttackRange())
      {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
      }
    }
  }

  public bool isPlayerInAttackRange()
  {
    return (Vector2.Distance(transform.position, Player.transform.position) < attackRange);
  }

  public bool isPlayerInDisengageRange()
  {
    return (Vector2.Distance(transform.position, Player.transform.position) < disengageRange);
  }

  public bool isPlayerInAggroRange()
  {
    return (Vector2.Distance(transform.position, Player.transform.position) < aggroRange);
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

  public void LookAt2D(Transform self, Transform target)
  {
    self.up = target.position - self.position;
  }

  public void LookAt2D(Transform self, Transform target, Vector3 offset)
  {
    self.up = target.position - self.position + offset;
  }
  public void giveXP() {
    Player.GetComponent<LevelSystem>().setCurrentXP(xp);
  }
}
