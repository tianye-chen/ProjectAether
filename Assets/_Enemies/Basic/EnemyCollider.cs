using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
  private GameObject parent;

  void Start() {
    Physics2D.IgnoreLayerCollision(7, 0); 
    parent = transform.parent.gameObject;
  }

  void Update()
  {
    transform.position = transform.parent.position;
  }

  public virtual void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      collision.gameObject.GetComponent<PlayerController>().TakeDamage(parent.GetComponent<EnemyBase>().atk);
    }
  }
}
