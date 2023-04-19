using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhioneCrystal : EnemyBase
{
  public int numShots;
  public float projectileSpeed;
  
  public GameObject KhioneProjectile;
  public GameObject BasicProjectile;

  private GameObject khioneController;
  private float attackTimer;

  public override void Start()
  {
    base.Start();

    khioneController = GameObject.Find("Khione(Clone)");
  }

  public override void FixedUpdate()
  {
    base.FixedUpdate();

    areaSlow();
    Attack();
  }

  public void areaSlow()
  {
    if (isPlayerInAttackRange())
    {
      Player.GetComponent<PlayerController>().stateMachine.AddState(new Slow_State(0.5f, 0.5f));
    }
  }

  public void Attack()
  {
    // fire projectiles in a 360 area
    if (attackTimer > attackSpeed)
    {
      for (int i = 0; i < 360; i += 360 / numShots)
      {
        StartCoroutine(pulsingAttack(i));
      }

      attackTimer = 0;
    }
    else
    {
      attackTimer += Time.deltaTime;
    }
  }

  IEnumerator pulsingAttack(int i)
  {
    GameObject projectile = Instantiate(BasicProjectile, transform.position, Quaternion.identity);
    BasicProjectile projectileComp = projectile.GetComponent<BasicProjectile>();
    projectileComp.useVelocity = true;
    projectileComp.setVelocity(Mathf.Cos(i * Mathf.Deg2Rad) * projectileSpeed, Mathf.Sin(i * Mathf.Deg2Rad) * projectileSpeed);
    projectileComp.SetDamage(atk);

    yield return new WaitForSeconds(1f);

    // projectile moves back
    if (projectile != null)
    {
      projectileComp.setVelocity(-projectileComp.xVelocity, -projectileComp.yVelocity);
    }

    yield return new WaitForSeconds(1f);
    
    if (projectile != null)
    {
      projectileComp.Delete();
    }
  }

  public override void Die()
  {
    Instantiate(KhioneProjectile, transform.position, Quaternion.identity);
    khioneController.GetComponent<KhioneController>().crystalDestroyed(this);
    base.Die();
  }
}
