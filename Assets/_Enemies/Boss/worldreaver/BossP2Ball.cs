using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossP2Ball : MonoBehaviour
{
  // public variables
  public float FireRate = 1f;
  public GameObject DefaultBullet;
  public Rigidbody2D rigid;
  public GameObject owner;
  public int numShots = 5;
  public Color orbColor;

  // private variables
  private GameObject InstBullet;
  private bool IsAttack = false;
  private float speed = 10;
  private float ballCount;
  private bool isEvenlySpaced = false;
  private float ownerX;
  private float xModifier = -1.25f;
  private float ownerY;
  private float yModifier = -0.75f;
  private float timer = 0f;
  private float timeAlive = 0f;
  private float lerpTimer = 0f;
  void Start()
  {
    if (rigid == null)
      rigid = GetComponent<Rigidbody2D>();
    if (owner != null)
    {
      ownerX = owner.transform.position.x + xModifier;
      ownerY = owner.transform.position.y + yModifier;
    }

    ballCount = GameObject.FindGameObjectsWithTag("WorldReaverOrb").Length;
  }

  private void FixedUpdate()
  {
    timeAlive += Time.deltaTime;
    if (timeAlive > 5f && IsAttack)
    {
      Destroy(gameObject);
    }

    if (owner == null)
    {
      Destroy(gameObject);
    } 
    else if (!IsAttack)
    {
      transform.RotateAround(new Vector3(ownerX, ownerY, -2), new Vector3(0, 0, -2), Time.deltaTime / 2 * 90f);

      // calculate ball positions if they are not evenly spaced
      if (ballCount > 1 && !isEvenlySpaced)
      {
        float angleStep = 360f / ballCount;
        float angle = angleStep * transform.GetSiblingIndex();
        Vector2 dir = new Vector2(Mathf.Sin((angle * Mathf.PI) / 180f), Mathf.Cos((angle * Mathf.PI) / 180f));
        Vector2 pos = dir * 6;

        transform.position = Vector2.Lerp(transform.position, new Vector2(ownerX, ownerY) + pos, Time.deltaTime * speed);

        if (lerpTimer > 0.5f)
        {
          isEvenlySpaced = true;
          lerpTimer = 0f;
        }
        else
          lerpTimer += Time.deltaTime;
      }

      // if owner position changes, then recalculate the ball positions
      if (Vector2.Distance(new Vector2(ownerX - xModifier, ownerY - yModifier), owner.transform.position) > 0.1f)
      {
        ownerX = owner.transform.position.x + xModifier;
        ownerY = owner.transform.position.y + yModifier;
        isEvenlySpaced = false;
      }

    }

    if (timer > FireRate) // Prevents firing when the object is outside the boundary
    {
      gameObject.GetComponent<AudioSource>().Play();
      for (float i = 0; i <= 360; i += 360f / numShots)
      {
        InstBullet = Instantiate(DefaultBullet, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.identity);
        InstBullet.GetComponent<BulletController>().SetInstObject("Boss");

        SpriteRenderer bulletSprite = InstBullet.GetComponent<SpriteRenderer>();
        bulletSprite.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        if (orbColor != new Color(0, 0, 0, 0))
          bulletSprite.color = orbColor;

        InstBullet.transform.eulerAngles = Vector3.forward * i;
      }
      timer = 0;
    }
    else
      timer += Time.deltaTime;

    if (IsAttack) // If the current instance is part of an attack of boss
    {
      transform.Translate(Vector2.up * 0.5f / speed);
    }
  }

  public void setOrbColor(Color color)
  {
    orbColor = color;
    GetComponent<SpriteRenderer>().color = orbColor;
  }

  public void SetIsAttack(bool val)
  {
    IsAttack = val;
  }
}
