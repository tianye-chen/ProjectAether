using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBase : MonoBehaviour
{
  // public variables

  // 1 = west 2 = east 3 = north 4 = south
  public int direction = 2;
  public float maxHealth, maxSpeed, maxAtk, maxDef, maxAccuracy, maxMana;
  public float health, speed, atk, def, accuracy, mana;
  //public float minimumXP, maximumXP,currentXP;
  public bool isInvulnerable;

  // assets
  public Rigidbody2D rigid;
  public HealthBar healthBar;
  public HealthBar healthBar2;

  public ManaBar manaBar;

  public bool defeated = false;

  public StateMachine stateMachine;

  // Start is called before the first frame update
  public virtual void Start()
  {
    SetStats();
    if (rigid == null)
      rigid = GetComponent<Rigidbody2D>();
    if (stateMachine == null)
      stateMachine = new StateMachine(this);
    transform.position = new Vector3(transform.position.x, transform.position.y, -2);
    LoadPlayer();
  }

  public virtual void FixedUpdate()
  {
    stateMachine.UpdateStates();
  }

  public virtual void Update()
  {
    transform.position = new Vector3(transform.position.x, transform.position.y, -2);
    SavePlayer();
  }

  public void SetStats()
  {
    mana = maxMana;
    health = maxHealth;
    speed = maxSpeed;
    atk = maxAtk;
    def = maxDef;
    accuracy = maxAccuracy;
    //sets healthbar maximum health base on the player maxHealth stat
    if (gameObject.tag == "Player")
    {
      healthBar.SetMaxHealth(maxHealth);
      healthBar2.SetMaxHealth(maxHealth);
      manaBar.SetMaxMana(maxMana);
    }

  }
  /*public void UpdateLevel(float minimum, float maximum, float current, int lvl) {
      minimumXP = minimum;
      maximumXP = maximum;
      currentXP = current;
      level = lvl;
  } */

  public virtual void TakeDamage(float damage)
  {

    if (!isInvulnerable)
    {
      GameObject damageDisplay = (GameObject)Instantiate(Resources.Load("DamageNumber"), transform.position, Quaternion.identity);

      health -= damage;
      //controls health bar
      if (gameObject.tag == "Player")
      {
        health = (int)health;
        damage = (int)damage;
        healthBar.SetHealth(health);
        healthBar2.SetHealth(health);
      }

      damageDisplay.GetComponent<DamageNumber>().SetDamageNumber(damage);
    }

    if (health <= 0)
    {
      Die();
    }
  }

  public virtual void HealSelf(float heal)
  {
    GameObject healDisplay = (GameObject)Instantiate(Resources.Load("DamageNumber"), transform.position, Quaternion.identity);
    healDisplay.GetComponent<DamageNumber>().SetHealingNumber(heal);
    health += heal;
    if (health > maxHealth)
      health = maxHealth;

    if (gameObject.tag == "Player")
    {
      healthBar.SetHealth(health);
      healthBar2.SetHealth(health);
    }
  }

  public virtual void depleteMana(float manaCost)
  {
    mana -= manaCost;
    if (mana < 0)
      mana = 0;

    if (gameObject.tag == "Player")
    {
      manaBar.SetMana(mana);
    }
  }

  public virtual void regenerateMana(float manaRegen)
  {
    mana += manaRegen;
    if (mana > maxMana)
      mana = maxMana;

    if (gameObject.tag == "Player")
    {
      manaBar.SetMana(mana);
    }
  }

  public bool checkMana(float manaCost)
  {
    if (mana >= manaCost)
      return true;
    else
      return false;
  }

  public virtual void Die()
  {
    if (gameObject.tag == "Enemy")
    {
      gameObject.GetComponent<EnemyBase>().giveXP();
      Destroy(gameObject);
    }
    if (gameObject.tag == "Player")
    {
      SceneManager.LoadScene("Lost Scene");
    }
  }
  public void SavePlayer()
  {
    if (gameObject.tag == "Player")
      SaveSystem.SavePlayer(this);
  }

  public void LoadPlayer()
  {
    if (gameObject.tag == "Player")
    {
      PlayerData data = SaveSystem.LoadPlayer();
      maxHealth = data.maxHealth;
      maxSpeed = data.maxSpeed;
      maxAtk = data.maxAtk;
      maxDef = data.maxDef;
      maxAccuracy = data.maxAccuracy;
      maxMana = data.maxMana;
      health = data.health;
      speed = data.speed;
      atk = data.atk;
      def = data.def;
      accuracy = data.accuracy;
      mana = data.mana;


      healthBar.SetMaxHealth(maxHealth);
      healthBar2.SetMaxHealth(maxHealth);
      healthBar.SetHealth(health);
      healthBar2.SetHealth(health);
      manaBar.SetMaxMana(maxMana);
      manaBar.SetMana(mana);
    }

    /*minimumXP = data.minimumXP;
    maximumXP = data.maximumXP;
    currentXP = data.currentXP;
    level = data.level;*/
  }
}