using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhioneController : EnemyBase
{
  public GameObject KhioneCrystal;
  public int numCrystalsToSpawn;

  // for spiral attack
  public int numShots_Spiral;

  // for blue projectile attack
  public int numTimesToAttack_BlueProjectile;
  public int numShots_BlueProjectile;

  public GameObject BasicProjectile;
  public GameObject KhioneProjectile;

  private List<KhioneCrystal> crystals = new List<KhioneCrystal>();
  private int phase = 1;
  private float attackTimer;
  private bool IsAttacking = false;

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
    switch (phase)
    {
      case 1:
        phase1();
        break;
    }
  }

  public void phase1()
  {
    // fire projectiles in a 360 area
    if (attackTimer > attackSpeed)
    {
      if (!IsAttacking)
      {
        StartCoroutine(BlueProjectileAttack());
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
    float innerAttackSpeed = 0.1f;
    float innerAttackSpeedTimer = 0;
    float innerAttackDuration = 10f;
    float innerAttackDurationTimer = 0;
    float projectileSpeed = 4f;
    int attackOffset = 0;
    int attackOffsetAmount = 15;

    while (innerAttackDuration > innerAttackDurationTimer)
    {
      if (innerAttackSpeedTimer > innerAttackSpeed)
      {
        for (int i = 0 + attackOffset; i < 360 + attackOffset; i += (360) / numShots_Spiral)
        {
          GameObject projectile = Instantiate(BasicProjectile, transform.position, Quaternion.identity);
          BasicProjectile projectileComp = projectile.GetComponent<BasicProjectile>();
          projectileComp.SetColor(new Color(0.6f, 0.63f, 0.92f));
          projectileComp.useVelocity = true;
          projectileComp.setVelocity(Mathf.Cos(i * Mathf.Deg2Rad) * projectileSpeed, Mathf.Sin(i * Mathf.Deg2Rad) * projectileSpeed);
          projectileComp.SetDamage(1);
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

  IEnumerator BlueProjectileAttack()
  {
    IsAttacking = true;

    for (int i = 0; i < numTimesToAttack_BlueProjectile; i++)
    {
      for (int j = 0; j < numShots_BlueProjectile; j++)
      {
        StartCoroutine(SummonBlueProjectile(i % 2 == 0 ? j : -j));
        yield return new WaitForSeconds(0.1f);
      }

      yield return new WaitForSeconds(0.75f);
    }

    attackTimer = 0;
    IsAttacking = false;
    
    // helper function to summon a blue projectile 
    IEnumerator SummonBlueProjectile(int i)
    {
      float lifespan = 5f;
      float lifespanTimer = 0;
      float gravityOnTime = 2f;
      float yVelocity = 3f;

      GameObject projectile = Instantiate(KhioneProjectile, transform.position, Quaternion.identity);
      KhioneProjectile projectileComp = projectile.GetComponent<KhioneProjectile>();
      projectileComp.useMonoSprite();
      projectileComp.SetColor(new Color(0, 1, 1f));
      projectileComp.setVelocity(i, yVelocity);

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


}
