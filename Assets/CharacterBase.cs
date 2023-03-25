using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBase : MonoBehaviour
{
    // public variables
    public bool attacking = false, usingAbility = false, beingHit = false, dodging = false,  blocking = false;

    //public List<Ability> abilities = new List<Ability>();

    // 1 = west 2 = east 3 = north 4 = south
    public int direction = 2;
    public bool initiatedBlocking = false;
    public float maxHealth, maxSpeed, maxAtk, maxDef;
    public float health, speed, atk, def;
    public bool isInvulnerable;

    // assets
    //public Ability generalBoost, EnergyBall, SuperBlock;
    public Animator animator;
    public Rigidbody2D rigid;

    public bool defeated = false;

    //public RegularAbilities myRegularAbilities;


    // Start is called before the first frame update
    public virtual void Start()
    {
        SetStats();
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
    }

    public virtual void FixedUpdate()
    {
        
    }

    
    public bool CantMove()
    {
        return attacking || usingAbility || beingHit || dodging || blocking; 
    }
    public void BecomeIdle()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        SetAnimation("idle");
    }
    public void SetStats()
    {
        health = maxHealth;
        speed = maxSpeed;
        atk = maxAtk;
        def = maxDef;
    }
    public void SetAnimation(string animation)
    {
        string [] animationStates = new string[7] { "charge", "walk", "release", "punch", "block", "idle", "dodging"};
        foreach (string i in animationStates) animator.SetInteger(i, 0);
        animator.SetInteger(animation, direction);
        
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
}