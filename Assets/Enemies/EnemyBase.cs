using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    // public variables
    public float aggroRange;
    public float disengageRange;
    public float attackRange;
    public float attackSpeed;

    
    // assets
    public GameObject Player;

    // private variables

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");

        health = maxHealth;
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
