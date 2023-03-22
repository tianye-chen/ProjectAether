using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
  // Start is called before the first frame update
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
}
