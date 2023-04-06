using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    private float verticalMovement;
    private float horizontalMovement;

    public void Update ()
    {
        PlayerAction();
        PlayerMovement();
    }

    public void PlayerMovement()
    {
        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");

        // Flip sprite if moving left
        if (horizontalMovement < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (horizontalMovement > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }

        rigid.velocity = new Vector2(horizontalMovement * speed, verticalMovement * speed);
    }

    public void PlayerAction()
    {
      if (Input.GetMouseButtonDown(0))
      {
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
              hitCollider.gameObject.GetComponent<EnemyBase>().TakeDamage(1);
              Debug.Log(direction + " " + mousePos);
            }
          }
        }
      }
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(1);

            Debug.Log("colliding");
        }
    }
}
