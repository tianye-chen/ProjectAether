using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    private float verticalMovement;
    private float horizontalMovement;

    public override void FixedUpdate()
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
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(1);

            Debug.Log("colliding");
        }
    }
}
