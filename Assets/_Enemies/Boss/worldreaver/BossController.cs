using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Boss Controller for World Reaver
 *  
 *  Boss will spawn with initOrb number of rotating orbs
 *  Boss will perform one of 5 attacks at random:
 *      1. Wave projectiles will spawn above the player and move down
 *      2. The boss will fire a number of waves at quick succession in a random direction and 
 *         fires several waves instantly in a 180 degree arc at the end of the attack
 *      3. The boss will spawn projectiles spiraling outwards from the boss
 *      4. The boss will spawn 2 orbs that will move outwards from the boss and fire projectiles at regular intervals
 *      5. The boss will charge up a number of projectiles in a circle around the boss and fire them all at once with great speed
 * 
 *  Boss will perform attack twice as fast when below 50% hp
 *  
 *  default common values:
 *      maxHealth = 100
 *      speed = 0
 *      aggroRange = 0
 *      disengageRange = 0
 *      attackRange = 0
 *      attackSpeed = 4 
 */

public class BossController : EnemyBase
{
  // public variables
  public int initOrbs;

  // private variables
  private GameObject InstBullet; // Used to operate on instanced objects
  private bool IsAttacking = false;
  private float AttackCooldownTimer = 0; // Used to determine the interval which attacks will occur
  private bool ReadyToFire = false; // Used for blast attack

  // assets
  public GameObject DefaultBullet;
  public GameObject BossP2Ball;
  public GameObject BossP2Wave_1;
  public GameObject BossP2Wave_2;
  public Animator BossTransitions;

  public override void Start()
  {
    if (rigid == null)
      rigid = GetComponent<Rigidbody2D>();

    if (Player == null)
      Player = GameObject.FindGameObjectWithTag("Player");

    health = maxHealth;

    // spawns initial rotating orbs
    for (int i = 0; i < initOrbs; i++)
    {
      GameObject orb = Instantiate(BossP2Ball, transform.position, Quaternion.identity);
      orb.GetComponent<BossP2Ball>().owner = gameObject;
    }
  }

  public override void FixedUpdate()
  {
    initiateBoss();
  }

  private void initiateBoss()
  {
    if (health <= maxHealth * 0.50) // When below 50% hp, attack twice as fast
      attackSpeed = 2.5f;
    if (AttackCooldownTimer > attackSpeed && !IsAttacking)
    {
      float RandomNum = Random.Range(0, 5);
      Debug.Log("New attack cycle with attack #" + RandomNum);
      int rounded = Mathf.RoundToInt(RandomNum);
      switch (rounded)
      {
        case (0):
          StartCoroutine(WaveAttack_1());
          break;
        case (1):
          StartCoroutine(SpiralAttack());
          break;
        case (2):
          StartCoroutine(WaveAttack_2());
          break;
        case (3):
          StartCoroutine(SummonAttack());
          break;
        case (4):
          StartCoroutine(BlastAttack());
          break;
        default:
          break;
      }
      AttackCooldownTimer = 0;
    }
    else
      AttackCooldownTimer += Time.deltaTime;
  }

  IEnumerator WaveAttack_1()  // Wave attacks will fall down from above the player
  {
    IsAttacking = true;
    BossTransitions.SetBool("ArmRaised", true);
    float AttackTimer = 0;
    float AttackDuration = 0;
    while (AttackDuration <= 10)
    {
      yield return null;
      if (AttackTimer > 0.3f)
      {
        Instantiate(BossP2Wave_1, new Vector2(Random.Range(Player.transform.position.x - 6, Player.transform.position.x + 6), Player.transform.position.y + 12), Quaternion.identity);
        AttackTimer = 0;
      }
      else
        AttackTimer += Time.deltaTime;
      AttackDuration += Time.deltaTime;
    }
    IsAttacking = false;
    BossTransitions.SetBool("ArmRaised", false);
    AttackCooldownTimer = 0;
  }

  IEnumerator WaveAttack_2() // The boss will rapidly fire wave attacks in a 180 degree area
  {
    IsAttacking = true;
    float AttackDuration = 0;
    BossTransitions.SetBool("IsSlashing", true);
    while (AttackDuration <= 10)
    {
      yield return new WaitForSeconds(0.1f);
      InstBullet = Instantiate(BossP2Wave_2, new Vector2(transform.position.x - 1.25f, transform.position.y - 1.75f), Quaternion.identity);
      InstBullet.transform.eulerAngles = Vector3.forward * Random.Range(-90, 90);
      AttackDuration += 0.1f;
    }
    BossTransitions.SetBool("IsSlashing", false);
    yield return new WaitForSeconds(0.6f);
    BossTransitions.SetBool("ArmRaised", true);
    yield return new WaitForSeconds(0.2f);
    BossTransitions.SetBool("ArmRaised", false);
    for (int i = -90; i <= 90; i += 25) // Final instance of this boss attack, instantly fires several fire waves in a 180 degree area
    {
      InstBullet = Instantiate(BossP2Wave_2, new Vector2(transform.position.x - 1.25f, transform.position.y - 1.75f), Quaternion.identity);
      InstBullet.transform.eulerAngles = Vector3.forward * i;
    }
    IsAttacking = false;
    AttackCooldownTimer = 0;
  }

  IEnumerator SpiralAttack() // The boss will summon bullets which will travel in 2 opposite spirals
  {
    IsAttacking = true;
    BossTransitions.SetBool("IsStance", true);
    float AttackDuration = 0;
    while (AttackDuration <= 10)
    {
      yield return new WaitForSeconds(0.3f);
      for (int i = -1; i <= 1; i += 2)
      {
        InstBullet = Instantiate(DefaultBullet, new Vector2(transform.position.x - 1.25f, transform.position.y - 0.75f), Quaternion.identity);
        InstBullet.GetComponent<BulletController>().SetProjectileSpeed(1f);
        InstBullet.GetComponent<BulletController>().SetInstObject("BossP2Spiral");
        InstBullet.GetComponent<BulletController>().SetSpiralDirection(i);
      }
      AttackDuration += 0.3f;
    }
    IsAttacking = false;
    BossTransitions.SetBool("IsStance", false);
    AttackCooldownTimer = 0;
  }

  IEnumerator SummonAttack() // The boss will summon 2 yellow orbs which will move 2 random directions firing bullets
  {
    IsAttacking = true;
    BossTransitions.SetBool("IsStance", true);
    float AttackDuration = 0;
    while (AttackDuration <= 10)
    {
      yield return new WaitForSeconds(3f);
      for (int i = -1; i <= 1; i += 2)
      {
        InstBullet = Instantiate(BossP2Ball, new Vector2(transform.position.x - 1.25f, transform.position.y - 0.75f), Quaternion.identity);
        InstBullet.transform.eulerAngles = Vector3.forward * Random.Range(0, 360);
        InstBullet.GetComponent<BossP2Ball>().SetIsAttack(true);
      }
      AttackDuration += 3f;
    }
    IsAttacking = false;
    BossTransitions.SetBool("IsStance", false);
    AttackCooldownTimer = 0;
  }

  IEnumerator BlastAttack() // The boss will first charge up a number of bullets in a circle around him, then fire them all at once
  {
    IsAttacking = true;
    BossTransitions.SetBool("IsStance", true);
    float AttackDuration = 0;
    float AttackInterval = 3f;
    while (AttackDuration <= 10)
    {
      yield return new WaitForSeconds(AttackInterval);
      ReadyToFire = false;
      for (int i = 0; i <= 360; i += Random.Range(10, 20))
      {
        InstBullet = Instantiate(DefaultBullet, new Vector2(transform.position.x - 1.25f, transform.position.y - 0.75f), Quaternion.identity);
        InstBullet.GetComponent<BulletController>().SetProjectileSpeed(1f);
        InstBullet.transform.eulerAngles = Vector3.forward * i;
        InstBullet.transform.Translate(Vector2.up * 3.5f);
        InstBullet.GetComponent<BulletController>().SetInstObject("BossP2BlastWait");
        yield return new WaitForSeconds(0.05f);
      }
      yield return new WaitForSeconds(0.2f);
      ReadyToFire = true; // Tells BulletController.cs that the charge up is over and is ready to fire all the bullets
      AttackDuration += AttackInterval;
      AttackInterval -= 0.5f;
    }
    IsAttacking = false;
    BossTransitions.SetBool("IsStance", false);
    AttackCooldownTimer = 0;
  }

  public bool IsReadyToFire()
  {
    return ReadyToFire;
  }
}
