using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBase : MonoBehaviour
{
  // public variables
  public bool attacking = false, usingAbility = false, beingHit = false, dodging = false, blocking = false;

  //public List<Ability> abilities = new List<Ability>();

  // 1 = west 2 = east 3 = north 4 = south
  public int direction = 2;
  public bool initiatedBlocking = false;
  public float maxHealth, maxSpeed, maxAtk, maxDef, maxAccuracy, maxMana;
  public float health, speed, atk, def, accuracy, mana;
  public bool isInvulnerable;

  public enum selfElement { Water, Fire, Wind, Earth, Electricity };
  public selfElement SelfElement;

  // assets
  public Ability generalBoost, EnergyBall, SuperBlock;
  public Animator animator;
  public Rigidbody2D rigid;
  public HealthBar healthBar;
  public HealthBar healthBar2;

  public ManaBar manaBar;

  public bool defeated = false;

  // public RegularAbilities myRegularAbilities;

  public StateMachine stateMachine;

  // Start is called before the first frame update
  public virtual void Start()
  {
    SetStats();
    if (rigid == null)
      rigid = GetComponent<Rigidbody2D>();
    if (stateMachine == null)
      stateMachine = new StateMachine(this);
  }
 
  public virtual void FixedUpdate()
  {
    stateMachine.UpdateStates();
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
    mana = maxMana;
    health = maxHealth;
    speed = maxSpeed;
    atk = maxAtk;
    def = maxDef;
    //sets healthbar maximum health base on the player maxHealth stat
    if (gameObject.tag == "Player")
    {
      healthBar.SetMaxHealth(maxHealth);
      healthBar2.SetMaxHealth(maxHealth);
      manaBar.SetMaxMana(maxMana);
    }


  }
  public void SetAnimation(string animation)
  {
    string[] animationStates = new string[7] { "charge", "walk", "release", "punch", "block", "idle", "dodging" };
    foreach (string i in animationStates) animator.SetInteger(i, 0);
    animator.SetInteger(animation, direction);

  }

  public virtual void TakeDamage(float damage)
  {

    if (!isInvulnerable)
    {
      health -= damage;
      //controls health bar
      if (gameObject.tag == "Player")
      {
        healthBar.SetHealth(health);
        healthBar2.SetHealth(health);
      }
    }

    if (health <= 0)
    {
      Die();
    }
  }

  public virtual void Die()
  {
    if(gameObject.tag == "Enemy") {
      gameObject.GetComponent<EnemyBase>().giveXP();
    }
    Destroy(gameObject);
  }
  
}