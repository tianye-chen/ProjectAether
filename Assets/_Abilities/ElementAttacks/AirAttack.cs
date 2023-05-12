using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAttack : BasicProjectile
{
  private float yPosOffset = 1f;
  private float xPosOffset = -0.6f;
  private GameObject parent;

  public override void Start()
  {
    base.Start();
    this.lifetime = 20f;
  }

  public override void move()
  {
    if (Vector2.Distance(transform.position, parent.transform.position) < 1f)
    {
      transform.position = Vector2.MoveTowards(transform.position, parent.transform.position, -(Time.deltaTime * 10f));
    }
    else if (Vector2.Distance(transform.position, parent.transform.position) > 4f)
    {
      transform.position = Vector2.MoveTowards(transform.position, parent.transform.position, Time.deltaTime * 10f);
    }

    transform.RotateAround(parent.transform.position + new Vector3(xPosOffset, yPosOffset), new Vector3(0, 0, 1), Time.deltaTime * 200f);
    transform.eulerAngles = new Vector3(0, 0, 0);
  }

  public void SetParent(GameObject newParent)
  {
    parent = newParent;
  }

  public override void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Enemy")
    {
      collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
    }
  }
}
