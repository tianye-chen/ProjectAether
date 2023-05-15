using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
  // public variables
  public float verticalMove;
  public Rigidbody2D rigid;
  public Sprite BossP2Ball_Sprite;
  public Sprite RedBullet_Sprite;
  public GameObject BossP2;
  public AudioClip BlastSpawn_Sound;
  public AudioClip BlastShoot_Sound;
  public State stateEffect;

  // private variables
  private string instObject;
  private float projectileSpeed = 1;
  private float SpiralMove = 0;
  private float SpiralSpeed = 0;
  private int SpiralDir = 1;
  private bool BlastSpawn = true;
  private float damage = 10;

  void Start()
  {
    if (rigid == null)
      rigid = GetComponent<Rigidbody2D>();

    BossP2 = GameObject.Find("worldreaver");

    if (BossP2 == null)
      BossP2 = GameObject.Find("worldreaver(Clone)");
  }

  void FixedUpdate()
  {
    switch (instObject)
    {
      case ("BossP2Spiral"):
        gameObject.GetComponent<SpriteRenderer>().sprite = BossP2Ball_Sprite;
        gameObject.GetComponent<Renderer>().material.color = Color.HSVToRGB(0, 0.45f, 1);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.2498093f, 0.2494715f);
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-0.005095348f, 0.004776936f);
        gameObject.transform.localScale = new Vector3(2, 2, 0);
        SpiralSpeed += Time.deltaTime + Time.deltaTime * projectileSpeed / 20;

        float x = Mathf.Cos(SpiralSpeed) * SpiralMove; // (Cos(SpiralSpeed), Sin(SpiralSpeed)) will create a circle pattern
        float y = Mathf.Sin(SpiralSpeed) * SpiralMove; // Multiplying it with SpiralMove will create a spiral pattern

        transform.Translate(new Vector2(x * SpiralDir, -y * SpiralDir)); // Move the bullet to along the path of the pattern
        SpiralMove += 0.0005f; // Slightly increasing the value to create a spiral pattern
        if (gameObject.transform.position.y <= -15)
          Destroy(gameObject);
        break;
      case ("BossP2BlastWait"):
        gameObject.GetComponent<SpriteRenderer>().sprite = RedBullet_Sprite;
        if (BlastSpawn)
        {
          BlastSpawn = false;
          StartCoroutine(BlastAttackWait());
        }
        break;
      case ("BossP2BlastMove"):
        transform.Translate(Vector2.up * projectileSpeed);
        if (transform.position.x > 20 || transform.position.x < -20 || transform.position.y > 20 || transform.position.y < -20)
          Destroy(gameObject);
        break;
      default:
        transform.Translate(Vector2.up * 0.1f);
        break;
    }

    // destroy bullet after 20 secs
    Destroy(gameObject, 20);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    switch (collision.gameObject.tag)
    {
      case ("Terrain"):
        Destroy(gameObject);
        break;
      case ("Player"):
        Destroy(gameObject);
        switch (instObject)
        {
          case ("BossP2BlastMove"):
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(40);
            break;
          case ("BossP2Spiral"):
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(25);
            break;
          default:
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            break;
        }
        if (stateEffect != null)
        {
          collision.gameObject.GetComponent<PlayerController>().stateMachine.AddState(stateEffect);
        }
        break;
      default:
        break;
    }
  }

  public void SetInstObject(string obj)
  {
    instObject = obj;
  }

  public void SetSpiralDirection(int val)
  {
    SpiralDir = val;
  }

  public void SetProjectileSpeed(float val)
  {
    projectileSpeed = val;
  }

  IEnumerator BlastAttackWait()
  {
    yield return new WaitUntil(() => BossP2.GetComponent<BossController>().IsReadyToFire());
    instObject = "BossP2BlastMove";
  }
}
