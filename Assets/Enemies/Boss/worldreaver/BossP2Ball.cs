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

  // private variables
  private GameObject InstBullet;
  private bool IsAttack = false;
  private bool IsOverBoundary = false;
  private float speed = 10;
  private float ballCount;
  private bool isEvenlySpaced = false;
  private float ownerX;
  private float xModifier = -1.25f;
  private float ownerY;
  private float yModifier = -0.75f;
  private float timer = 0f;
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
    if (!IsAttack)
    {
      transform.RotateAround(new Vector2(ownerX, ownerY), new Vector3(0, 0, 1f), Time.deltaTime / 2 * 90f);

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
    if (timer > FireRate && !IsOverBoundary) // Prevents firing when the object is outside the boundary
    {
      gameObject.GetComponent<AudioSource>().Play();
      for (float i = 0; i <= 360; i += 360f / numShots)
      {
        InstBullet = Instantiate(DefaultBullet, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        InstBullet.GetComponent<BulletController>().SetInstObject("Boss");
        InstBullet.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
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

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Terrain")
    {
      IsOverBoundary = !IsOverBoundary;
      if (IsAttack)
      {
        Destroy(gameObject);
      }
    }
  }

  public void SetIsAttack(bool val)
  {
    IsAttack = val;
  }
}
