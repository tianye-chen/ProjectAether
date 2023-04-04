using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
  public Rigidbody2D rigid;
  public float damage = 1f;
  public Color projectileColor;

  // Start is called before the first frame update
  void Start()
  {
    if (rigid == null)
      rigid = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate()
  {
    transform.Translate(Vector2.up * 0.1f);
  }

  public void Delete()
  {
    Destroy(gameObject);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
    }

    if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Terrain")
    {
      Destroy(gameObject);
    }
  }

  public void SetColor(Color color)
  {
    projectileColor = color;
    GetComponent<SpriteRenderer>().color = projectileColor;
  }
}
