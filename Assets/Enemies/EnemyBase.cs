using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // public variables
    public float maxHealth;
    public float speed;
    public float aggroRange;
    public float disengageRange;
    public float attackRange;
    public float attackSpeed;
    public bool isInvulnerable;
    
    // assets
    public Rigidbody2D rigid;
    public GameObject Player;

    // private variables
    [SerializeField] protected float health;

    // Start is called before the first frame update
    public virtual void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");

        health = maxHealth;
    }

    public virtual void FixedUpdate()
    {
        
    }

    public virtual void TakeDamage(float damage){
       
        if (!isInvulnerable)
        {
            health -= damage;
            Debug.Log("Enemy TakeDamage");
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die(){
        Destroy(gameObject);
    }

    // ignore collision with player
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
