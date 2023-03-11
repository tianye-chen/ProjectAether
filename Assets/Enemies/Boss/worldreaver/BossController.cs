using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] public float MaxHealth = 500;
    [SerializeField] public float Health = 500;
    [SerializeField] public float FireRate = 1f;
    [SerializeField] public float RocketsFireRate = 2f;
    [SerializeField] public Rigidbody2D rigid;
    [SerializeField] public GameObject DefaultBullet;
    [SerializeField] public GameObject BossP2Ball;
    [SerializeField] public GameObject BossP2Wave_1;
    [SerializeField] public GameObject BossP2Wave_2;
    [SerializeField] public Animator BossTransitions;
    private GameObject InstBullet; // Used to operate on instanced objects
    private bool IsDead = false;
    private bool IsPhase2Dead = false;
    private bool IsAttacking = false;
    private int phase = 1;
    private float timer = 0f; // Used to determine the interval which the basic attack will occur
    private float BeamTimer = 0f; // Used to determine the interval which the beam attack will occur
    private float RocketTimer = 0f; // Used to determine the interval which the rocket attack will occur
    private float AttackCooldownTimer = 2; // Used to determine the interval which phase 2 attacks will occur
    private float AttackCooldownDuration = 5; // The time between attacks of phase 2
    private bool ReadyToFire = false; // Used for blast attack of phase 2
    private GameObject BeamTemp;

    private void Awake()
    {

    }

    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        MaxHealth = 500;
        Health = MaxHealth;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.496746f, 0.4308989f);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.1622606f, -0.004992545f);
        Instantiate(BossP2Ball, new Vector2(-5,7), Quaternion.identity);
        Instantiate(BossP2Ball, new Vector2(5, 7), Quaternion.identity);
    }

    private void FixedUpdate()
    {
        Phase2Boss();
    }

    //Spawns a bullet and rotate it to the angle parameter
    private void SpreadAttack(float angle)
    {
        InstBullet = Instantiate(DefaultBullet, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 2), Quaternion.identity);
        InstBullet.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 255);
        InstBullet.GetComponent<BulletController>().SetInstObject("Boss");
        InstBullet.transform.eulerAngles = Vector3.forward * angle;
    }

    private void Phase2Boss() 
    {
        if (Health <= MaxHealth * 0.50) // When below 50% hp, attack twice as fast
            AttackCooldownDuration = 2.5f;
        if (AttackCooldownTimer > AttackCooldownDuration && !IsAttacking)
        {
            float RandomNum = Random.Range(0, 5);
            Debug.Log("New attack cycle with attack #"+RandomNum);
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

    IEnumerator WaveAttack_1()  // Wave attacks will fall down from the top of the screen
    {
        IsAttacking = true;
        BossTransitions.SetBool("ArmRaised", true);
        float AttackTimer = 0;
        float AttackDuration = 0;
        while (AttackDuration <= 10)
        {
            yield return new WaitForEndOfFrame();
            if (AttackTimer > 0.3f)
            {
                Instantiate(BossP2Wave_1, new Vector2(Random.Range(-12, 12), 12), Quaternion.identity);
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
            InstBullet = Instantiate(BossP2Wave_2, new Vector2(0,5), Quaternion.identity);
            InstBullet.transform.eulerAngles = Vector3.forward * Random.Range(-90,90);
            AttackDuration += 0.1f;
        }
        BossTransitions.SetBool("IsSlashing", false);
        yield return new WaitForSeconds(0.6f);
        BossTransitions.SetBool("ArmRaised", true);
        yield return new WaitForSeconds(0.2f);
        BossTransitions.SetBool("ArmRaised", false);
        for (int i = -90; i <= 90; i += 25) // Final instance of this boss attack, instantly fires several fire waves in a 180 degree area
        {
            InstBullet = Instantiate(BossP2Wave_2, new Vector2(0, 5), Quaternion.identity);
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
            for (int i = -1; i <= 1; i+=2)
            {
                InstBullet = Instantiate(DefaultBullet, new Vector2(0, 6), Quaternion.identity);
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
        while(AttackDuration <= 10)
        {
            yield return new WaitForSeconds(3f);
            for (int i = -1; i <= 1; i+=2)
            {
                InstBullet = Instantiate(BossP2Ball, new Vector2(0, 6), Quaternion.identity);
                InstBullet.transform.eulerAngles = Vector3.forward * Random.Range(0,360);
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
                InstBullet = Instantiate(DefaultBullet, new Vector2(0,6), Quaternion.identity);
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

    public void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public bool IsReadyToFire() 
    {
        return ReadyToFire;
    }
}
