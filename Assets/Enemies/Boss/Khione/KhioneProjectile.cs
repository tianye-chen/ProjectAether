using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhioneProjectile : BasicProjectile
{
  public float attackRange;
  public float attackSpeed;
  public float attackSpeedTimer;
  public bool isAttacking = false;
  public Sprite monoSprite;

  public GameObject player;
  
  public override void Start()
  {
    useVelocity = true;
    yVelocity = yVelocity * 2;
    useCallback(projectileBehavior);

    base.Start();
    player = GameObject.FindGameObjectWithTag("Player");
    attackSpeed = 3f;
    attackRange = 5f;
    attackSpeedTimer = attackSpeed;
  }

  public void projectileBehavior()
  {
    // if the player is within range and attack cooldown is over and not currently attacking
    if (Vector2.Distance(transform.position, player.transform.position) < attackRange && attackSpeed < attackSpeedTimer && !isAttacking)
    {
      StartCoroutine(Attack());
      attackSpeedTimer = 0;
    } else
    {
      attackSpeedTimer += Time.deltaTime;
    }

    maintainRotation();
  }

  // Maintains the rotation of the projectile to always face the direction of travel
  public void maintainRotation()
  {
    transform.rotation = Quaternion.LookRotation(Vector3.forward, rigid.velocity);
  }

  IEnumerator Attack()
  {
    isAttacking = true;
    rigid.velocity = Vector2.zero;

    // rotate the sprite around 3 times over 1 second
    float time = 0;
    float duration = 1f;
    float angle = 0;
    float timesToRotate = 5;
    while (time < duration)
    {
      time += Time.deltaTime;
      angle = Mathf.Lerp(0, 360 * timesToRotate, time / duration);
      transform.rotation = Quaternion.Euler(0, 0, angle);
      yield return null;
    }

    // start moving towards the player at a slow speed
    rigid.velocity = (player.transform.position - transform.position).normalized * (yVelocity * 0.1f);

    yield return new WaitForSeconds(0.5f);

    // significantly increase speed
    rigid.velocity = (player.transform.position - transform.position).normalized * (yVelocity * 4f);
    yield return new WaitForSeconds(1.5f);

    // return to normal speed
    rigid.velocity /= 2;
    isAttacking = false;
  }

  public void useMonoSprite()
  {
    GetComponent<SpriteRenderer>().sprite = monoSprite;
  }

  // override the OnTriggerEnter2D method from BasicProjectile to add the bounce off terrain behavior
  public override void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Terrain")
    {
      // bounce off terrain at the direction of approach
      Vector2 direction = collision.ClosestPoint(transform.position) - (Vector2)transform.position;
      rigid.velocity = Vector2.Reflect(rigid.velocity, direction.normalized);
    } else if (collision.gameObject.tag == "Player")
    {
      collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
      if (stateEffect != null)
      {
        collision.gameObject.GetComponent<PlayerController>().stateMachine.AddState(stateEffect);
      }
    }
  }
}
