using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  Assassin
 * 
 *  Assassin will enter stealth mode when the player is within aggro range.
 *  When the player is within attack range, the assassin will appear at a random location
 *  within attack range and attack the player.
 *  The assassin will return to stealth mode after the attack.
 *  If the assassin takes damage while attacking, it will be stunned.
 *  
 *  default common variables:
 *    maxHealth = 10
 *    speed = 5
 *    aggroRange = 10
 *    disengageRange = 15
 *    attackRange = 3
 *    attackSpeed = 2
*/
public class AssassinEnemyController : EnemyBase
{
  // public variables
  public float stunTime = 2f;
  public float stealthTime = 2f;

  // state variables
  public AssassinStateController currentState;
  public AssassinStealthState stealthState = new AssassinStealthState();
  public AssassinStunnedState stunnedState = new AssassinStunnedState();
  public AssassinRegularState regularState = new AssassinRegularState();
  public AssassinAttackState attackState = new AssassinAttackState();


  // Start is called before the first frame update
  public override void Start()
  {
    base.Start();
    currentState = regularState;
    currentState.EnterState(this);
  }

  public override void FixedUpdate()
  {
    if (currentState != stunnedState && currentState != attackState)
    {
      // turn to face the player
      facePlayer();
    }
    currentState.Update(this);
  }

  public void SwitchState(AssassinStateController state)
  {
    currentState = state;
    currentState.EnterState(this);
  }

  public override void TakeDamage(float damage)
  {
    if (currentState == attackState)
    {
      SwitchState(stunnedState);
    }

    base.TakeDamage(damage);
  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Player" && currentState == attackState)
    {
      if (currentState == attackState)
      {
        collision.gameObject.GetComponent<PlayerController>().TakeDamage(1);
      }
    }
  }
}
