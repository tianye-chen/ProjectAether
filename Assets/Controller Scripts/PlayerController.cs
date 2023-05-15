using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
  [SerializeField]
  private Animator animator;
  private float verticalMovement;
  private float horizontalMovement;
  private float basicAttackCooldown = 1f;
  private float basicAttackTimer = 0f;

  // testing purposes only
  public GameObject testSpawnMob;

  public override void Update()
  {
    base.Update();
    PlayerAction();
    PlayerMovement();
  }

  public void PlayerMovement()
  {
    verticalMovement = Input.GetAxis("Vertical");
    horizontalMovement = Input.GetAxis("Horizontal");

    // Flip sprite if moving left
    if (horizontalMovement == 0 && verticalMovement == 0)
    {
      animator.SetBool("isRunning", false);
    }
    else if (horizontalMovement < 0)
    {
      GetComponent<SpriteRenderer>().flipX = true;
      animator.SetBool("isRunning", true);
    }
    else if (horizontalMovement > 0)
    {
      GetComponent<SpriteRenderer>().flipX = false;
      animator.SetBool("isRunning", true);
    }

    if (verticalMovement != 0)
    {
      animator.SetBool("isRunning", true);
    }

    rigid.velocity = new Vector3(horizontalMovement * speed, verticalMovement * speed, -1);
  }

  public void PlayerAction()
  {
    if (Input.GetMouseButtonDown(0) && basicAttackTimer <= 0f)
    {
      basicAttackTimer = basicAttackCooldown;
      animator.SetBool("isAttacking", true);

      // Get angle of mouse click relative to player
      Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector3 direction = mousePos - transform.position;
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


      // Damage enemies in range of player in direction of mouse click
      Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 5);
      foreach (Collider2D hitCollider in hitColliders)
      {
        if (hitCollider.gameObject.tag == "Enemy")
        {
          // Get angle of enemy relative to player
          Vector3 enemyPos = hitCollider.gameObject.transform.position;
          Vector3 enemyDirection = enemyPos - transform.position;
          float enemyAngle = Mathf.Atan2(enemyDirection.y, enemyDirection.x) * Mathf.Rad2Deg;

          // If enemy is within 45 degrees of mouse click, damage enemy
          if (Mathf.Abs(angle - enemyAngle) < 45)
          {
            hitCollider.gameObject.GetComponent<EnemyBase>().TakeDamage(5);
            Debug.Log(direction + " " + mousePos);
          }
        }
      }

      StartCoroutine(resetAttackAnimation());
    }
    else
    {
      basicAttackTimer -= Time.deltaTime;
    }

    if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKeyDown(KeyCode.LeftShift))
    {
      // Spawn test mob
      Instantiate(testSpawnMob, transform.position, Quaternion.identity);
    }
  }

  private IEnumerator resetAttackAnimation()
  {
    yield return new WaitForSeconds(0.4f);
    animator.SetBool("isAttacking", false);
  }
}
