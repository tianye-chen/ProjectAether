using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhioneController : EnemyBase
{
  public GameObject KhioneCrystal;
  public int numCrystalsToSpawn;

  // for spiral attack
  public int numShots_Spiral;
  public float attackSpeed_Spiral;
  public float attackDuration_Spiral;

  // for blue projectile attack
  public int numShots_BlueProjectile;

  public GameObject BasicProjectile;
  public GameObject KhioneProjectile;

  // for summoning blue skulls
  public GameObject BlueSkull;
  private bool hasSkullsBeenSpawned = false;

  private List<KhioneCrystal> crystals = new List<KhioneCrystal>();
  private int phase = 1;
  private float attackTimer;
  private bool IsAttacking = false;
  private bool hasCrystalPhaseStarted = false;
  private int currentAttackPattern = 0; 

  public override void Start()
  {
    base.Start();

    // spawn crystals in a circle around the boss
    float distanceFromBoss = 12;
    for (int i = 0; i < 360; i += 360 / numCrystalsToSpawn)
    {
      GameObject crystal = Instantiate(KhioneCrystal, transform.position, Quaternion.identity);
      crystal.transform.Translate(Mathf.Cos(i * Mathf.Deg2Rad) * distanceFromBoss, Mathf.Sin(i * Mathf.Deg2Rad) * distanceFromBoss, 0);
      KhioneCrystal crystalComp = crystal.GetComponent<KhioneCrystal>();
      crystals.Add(crystalComp);
    }
  }

  public override void FixedUpdate()
  {
    base.FixedUpdate();

    initBossBehaviors();
  }

  public void initBossBehaviors()
  {
    // spawn blue skulls when boss is at 50% health
    if (health / maxHealth <= 0.5f && !hasSkullsBeenSpawned)
    {
      hasSkullsBeenSpawned = true;
      spawnBlueSkulls();
    }
    // start crystal phase when boss is at 25% health
    else if (health / maxHealth <= 0.25 && !hasCrystalPhaseStarted)
    {
      hasCrystalPhaseStarted = true;
      isInvulnerable = true;
      phase = 2;
    }

    if (isInvulnerable)
    {
      GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
    } else 
    {
      GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    attackPatterns();
  }

  public void attackPatterns()
  {

    if (attackTimer > attackSpeed)
    {
      if (!IsAttacking)
      {
        // 2 attacks in phase 1, 3 attacks in phase 2
        switch (currentAttackPattern % (phase == 1 ? 2 : 3))
        {
          case 0:
            StartCoroutine(SpiralAttack());
            break;
          case 1:
            StartCoroutine(BlueProjectileAttack(0));
            break;
          case 2:
            StartCoroutine(BlueProjectileAttack(1));
            break;
        }

        currentAttackPattern++;
      }
    }
    else
    {
      attackTimer += Time.deltaTime;
    }
  }

  IEnumerator SpiralAttack()
  {
    IsAttacking = true;
    float innerAttackSpeedTimer = 0;
    float innerAttackDurationTimer = 0;
    float projectileSpeed = 4f;
    int attackOffset = 0;
    int attackOffsetAmount = 15;

    while (attackDuration_Spiral > innerAttackDurationTimer)
    {
      if (innerAttackSpeedTimer > attackSpeed_Spiral)
      {
        for (int i = 0 + attackOffset; i < 360 + attackOffset; i += (360) / numShots_Spiral)
        {
          GameObject projectile = Instantiate(BasicProjectile, transform.position, Quaternion.identity);
          BasicProjectile projectileComp = projectile.GetComponent<BasicProjectile>();
          projectileComp.SetColor(new Color(0.6f, 0.63f, 0.92f));
          projectileComp.useVelocity = true;
          projectileComp.setVelocity(Mathf.Cos(i * Mathf.Deg2Rad) * projectileSpeed, Mathf.Sin(i * Mathf.Deg2Rad) * projectileSpeed);
          projectileComp.SetDamage(atk * 2f);
        }

        attackOffset += attackOffsetAmount;
        innerAttackSpeedTimer = 0;
      }
      else
      {
        innerAttackSpeedTimer += Time.deltaTime;
      }

      innerAttackDurationTimer += Time.deltaTime;
      yield return null;
    }

    attackTimer = 0;
    IsAttacking = false;
  }

  IEnumerator BlueProjectileAttack(int pattern)
  {
    IsAttacking = true;
    int numTimesToAttack_BlueProjectile = pattern == 0 ? 5 : 100;

    // fire projectiles upwards with slight delay, alternating between left and right if pattern 0
    // if pattern 1, fire all projectile simultaneously
    for (int i = 0; i < numTimesToAttack_BlueProjectile; i++)
    {
      for (int j = pattern == 0 ? 0 : 1; j < numShots_BlueProjectile; j++)
      {
        StartCoroutine(SummonBlueProjectile(i % 2 == 0 ? j : -j));

        yield return pattern == 0 ? new WaitForSeconds(0.1f) : null;
      }

      yield return new WaitForSeconds(pattern == 0 ? 0.75f : 0.1f);
    }

    attackTimer = 0;
    IsAttacking = false;

    // helper function to summon a blue projectile 
    IEnumerator SummonBlueProjectile(int xVelocity)
    {
      float lifespan = pattern == 0 ? 5f : 10f;
      float lifespanTimer = 0;
      float gravityOnTime = 2f;
      float yVelocity = 3f;

      GameObject projectile = Instantiate(KhioneProjectile, transform.position, Quaternion.identity);
      KhioneProjectile projectileComp = projectile.GetComponent<KhioneProjectile>();
      projectileComp.useMonoSprite();
      projectileComp.SetColor(new Color(0, 1, 1f));
      projectileComp.setVelocity(xVelocity, yVelocity);
      projectileComp.SetDamage(atk * 3f);

      if (pattern == 1)
      {
        projectileComp.attackDisabled = true;
      }

      yield return new WaitForSeconds(1f);
      projectileComp.rigid.gravityScale = 1;

      projectileComp.useCallback(specialBehavior);

      // helper function to handle special behavior for the blue projectile
      void specialBehavior()
      {
        projectileComp.projectileBehavior();

        if (projectileComp.isAttacking || lifespanTimer > gravityOnTime)
        {
          projectileComp.rigid.gravityScale = 0;
        }

        if (lifespanTimer > lifespan)
        {
          Destroy(projectile);
        }
        else
        {
          lifespanTimer += Time.deltaTime;
        }
      }
    }
  }

  public void spawnBlueSkulls()
  {
    GameObject skull;

    skull = Instantiate(BlueSkull, new Vector2(transform.position.x, transform.position.y - 0.75f), Quaternion.identity);
    skull.GetComponent<BlueSkull>().changeMoveDirection(new Vector2(0, 1));
    skull = Instantiate(BlueSkull, transform.position, Quaternion.identity);
    skull.GetComponent<BlueSkull>().changeMoveDirection(new Vector2(1, -1));
    skull = Instantiate(BlueSkull, transform.position, Quaternion.identity);
    skull.GetComponent<BlueSkull>().changeMoveDirection(new Vector2(-1, -1));
  }

  public void crystalDestroyed(KhioneCrystal crystal)
  {
    crystals.Remove(crystal);
    if (crystals.Count == 0)
    {
      isInvulnerable = false;
    }
  }

  public override void Die()
  {
    base.Die();
    // destroy all khione projectiles
    foreach (KhioneProjectile projectile in GameObject.FindObjectsOfType(typeof(KhioneProjectile)))
    {
      Destroy(projectile.gameObject);
    }

    // destroy all blue skulls
    foreach (BlueSkull skull in GameObject.FindObjectsOfType(typeof(BlueSkull)))
    {
      Destroy(skull.gameObject);
    }

    Destroy(gameObject);
  }
}
