using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : MonoBehaviour
{
  private float yPosOffset = 1f;
  private float xPosOffset = -0.6f;
  private float spreadRadius = 10f;
  private float lifetime = 10f;
  private float damage = 0.05f;
  private GameObject[] enemies;

  void Start()
  {
    transform.position = new Vector3(transform.position.x + xPosOffset, transform.position.y + yPosOffset, transform.position.z);
  }

  void Update()
  {
    FindClosestEnemy();
    lifetime -= Time.deltaTime;
    if (lifetime <= 0)
    {
      Destroy(gameObject);
    }
  }

  private void FindClosestEnemy()
  {
    GameObject closestEnemy = null;
    enemies = GameObject.FindGameObjectsWithTag("Enemy");

    foreach (GameObject enemy in enemies)
    {
      float distance = Vector3.Distance(transform.position, enemy.transform.position);
      if (distance < spreadRadius)
      {
        closestEnemy = enemy;
      }
    }

    if (closestEnemy != null)
    {
      transform.position = closestEnemy.transform.position;
    }
  }

  public void OnTriggerStay2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Enemy")
    {
      collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
      Debug.Log(collision.GetComponent<EnemyBase>().health);
    }
  }

  public void SetDamage(float damage)
  {
    this.damage = damage;
  }
}
