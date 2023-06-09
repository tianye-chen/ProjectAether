using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
  public float damage = 1;
  private float timer = 0;

  void Start()
  {
    gameObject.GetComponent<BoxCollider2D>().enabled = false;
    transform.Rotate(0, 0, -90);
    float height = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
    transform.Translate(-height / 2, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    if (timer > 2f)
    {
      Destroy(gameObject);
    }
    else if (timer > 1f)
    {
      gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    timer += Time.deltaTime;
  }

  public void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
    }
  }
}
