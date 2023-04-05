using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 *  Caster Enemy
 * 
 *  Caster enemy that casts a spell that damages the player
 *  The spell is cast in a circular area at the player's location
 *  The caster may caster multiple spells at the same time, centered at the player's location
 *  If the caster is hit while casting a spell, it will be stunned for a short time
 *  If the player gets too close to the caster, it will fire projectiles at the player and move away
 * 
 *  default common values:
 *      maxHealth = 10
 *      speed = 4
 *      aggroRange = 15
 *      disengageRange = 15
 *      attackRange = 8
 *      attackSpeed = 2
*/
public class CasterEnemyController : EnemyBase
{
  // public variables
  public float stunTime = 3f;
  public float castTime = 2f;
  public float castRadius = 5f;
  public float castDamage = 2f;
  public float castCooldown = 4f;
  public int numCasts = 3;
  public int numProjectiles = 3;

  // state variables
  public CasterStateController currentState;
  public CasterCastingState castingState = new CasterCastingState();
  public CasterStunnedState stunnedState = new CasterStunnedState();
  public CasterRegularState regularState = new CasterRegularState();

  //assets
  public GameObject areaMarker;
  public GameObject castingObject;
  public GameObject projectileObject;

  // Start is called before the first frame update
  public override void Start()
  {
    base.Start();
    // set castTimer to castCooldown so that the enemy can cast immediately
    regularState.castTimer = castCooldown;
    currentState = regularState;
    currentState.EnterState(this);
  }
  public override void FixedUpdate()
  {
    base.FixedUpdate();

    if (currentState != stunnedState)
    {
      facePlayer();
    }
    currentState.Update(this);
  }

  public void SwitchState(CasterStateController state)
  {
    currentState = state;
    currentState.EnterState(this);
  }

  public override void TakeDamage(float damage)
  {
    // stun the enemy if it is casting
    if (currentState == castingState)
    {
      currentState = stunnedState;
      currentState.EnterState(this);
    }

    base.TakeDamage(damage);
  }
}
