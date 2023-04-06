using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Teleport Mage
 * 
 * Teleports to a random location within attackRange and spawns an orb that moves towards the player
 * 
 * default common values:
 *      maxHealth = 20
 *      maxSpeed = 2
 *      maxAtk = 15
 *      maxDef = 5
 *      aggroRange = 10
 *      disengageRange = 15
 *      attackRange = 7.5
 *      attackSpeed = 3 
*/
public class TeleportMage : EnemyBase
{
  private float teleportTimer = 0;

  // asset variables
  public GameObject attackOrb;
  private GameObject InstOrb;

  // Update is called once per frame
  public override void FixedUpdate()
  {
    base.FixedUpdate();
    facePlayer();
    move();
  }

  public override void move()
  {
    base.move();

    if (teleportTimer > 2 && playerInAttackRange)
    {
      Teleport();
      Attack();
      teleportTimer = 0;
    }
    else
    {
      teleportTimer += Time.deltaTime;
    }
  }

  private void Teleport()
  {
    // teleport to a random location within attackRange
    transform.position = new Vector2(Random.Range(Player.transform.position.x - attackRange + 2, Player.transform.position.x + attackRange - 2),
    Random.Range(Player.transform.position.y - attackRange + 2, Player.transform.position.y + attackRange - 2));
  }

  private void Attack()
  {
    // spawn an orb that moves towards player
    InstOrb = Instantiate(attackOrb, transform.position, Quaternion.identity);
    BossP2Ball orbComponent = InstOrb.GetComponent<BossP2Ball>();

    LookAt2D(InstOrb.transform, Player.transform);

    // set orb color to green


    orbComponent.SetIsAttack(true);
    orbComponent.owner = gameObject;
    orbComponent.setOrbColor(new Color(0, 255, 0));
    orbComponent.numShots = 3;
  }
}
