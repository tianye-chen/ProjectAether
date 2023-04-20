using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSkull : EnemyBase
{

  public KhioneController khioneController;
  public BasicProjectile projectile;
  public int numProjectiles;
  public Sprite projectileSprite;

  private float attackTimer = 0f;
  private Vector2 moveDirection = new Vector2(1, 0);

  public override void Start()
  {
    base.Start();    
  }

  public override void FixedUpdate()
  {
    base.FixedUpdate();

    move();
  }

  public override void move()
  {
    if (Vector3.Distance(transform.position, khioneController.transform.position) < 10)
    {
      transform.Translate(moveDirection * speed * Time.deltaTime);
    }
    else
    {
      transform.RotateAround(khioneController.transform.position, Vector3.forward, 20 * Time.deltaTime);
      transform.eulerAngles = new Vector3(0, 0, 0);
      faceBoss();

      if (attackTimer > attackSpeed)
      {
        for (int i = 0; i < 15 * numProjectiles; i += 15)
        {
          BasicProjectile projectileInst = Instantiate(projectile, transform.position, Quaternion.identity);
          LookAt2D(projectileInst.transform, khioneController.transform);
          projectileInst.transform.Rotate(0, 0, i - 7.5f * (numProjectiles - 1));
          projectileInst.GetComponent<SpriteRenderer>().sprite = projectileSprite;
          projectileInst.GetComponent<BasicProjectile>().SetDamage(atk);
        }
        attackTimer = 0;
      }
      else
      {
        attackTimer += Time.deltaTime;
      }
    }
  }

  public void faceBoss()
  {
    if (khioneController.transform.position.x < transform.position.x)
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

  public void changeMoveDirection (Vector2 newDirection)
  {
    moveDirection = newDirection;
  }
}
