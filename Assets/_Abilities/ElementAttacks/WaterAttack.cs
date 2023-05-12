using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAttack : MonoBehaviour
{
  private float healStrength;
  private float healInterval = 1f;
  private float healRadius = 3f;
  private float lifetime = 10f;
  private GameObject parent;

  void Update()
  {
    healInterval -= Time.deltaTime;
    lifetime -= Time.deltaTime;
    if (healInterval <= 0)
    {
      healInterval = 1f;
      Heal();
    }

    if (lifetime <= 0)
    {
      Destroy(gameObject);
    }
  }

  private void Heal()
  {
    if (Vector3.Distance(parent.transform.position, transform.position) <= healRadius)
    {
      parent.GetComponent<CharacterBase>().health += healStrength;
    }
  }

  public void SetHealStrength(float strength)
  {
    healStrength = strength;
  }

  public void SetParent (GameObject newParent)
  {
    parent = newParent;
  }
}
