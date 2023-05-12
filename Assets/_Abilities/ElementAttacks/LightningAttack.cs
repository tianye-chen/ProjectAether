using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAttack : BasicProjectile
{
  public override void Start()
  {
    base.Start();
    this.speed = 1f;
  }

  public override void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Enemy")
    {
      collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
      Destroy(gameObject);
    }

    if (collision.gameObject.tag == "Terrain")
    {
      Destroy(gameObject);
    }
  }
}
