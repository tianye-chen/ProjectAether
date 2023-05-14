using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaMarker : MonoBehaviour
{
  // public variables
  public float duration;
  public float radius;
  public float damage;
  public GameObject InstObject;

  // private variables
  private float timer = 0f;

  void Start()
  {
    transform.localScale = new Vector3(radius, radius, 1);
  }

  void FixedUpdate()
  {
    timer += Time.deltaTime;
    if (timer > duration)
    {
      if (InstObject != null)
      {
        GameObject beam = Instantiate(InstObject, transform.position, Quaternion.identity);
        beam.GetComponent<BeamController>().damage = damage;
      }
      Destroy(gameObject);
    }
  }
}
