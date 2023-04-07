using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
  public Rigidbody2D rigid;
  public float damage = 1f;
  public Color projectileColor;
  public float speed = 0.1f;
  public float xVelocity = 0f;
  public float yVelocity = 1f;
  public bool useVelocity = false;
  public State stateEffect;
  public delegate void callbackHandler();
  public event callbackHandler callback;

  public virtual void Start()
  {
    if (rigid == null)
      rigid = GetComponent<Rigidbody2D>();
    if (useVelocity)
      rigid.AddForce(new Vector2(xVelocity * 100f, yVelocity * 100f));
  }

  void FixedUpdate()
  {
    if (callback != null)
    {
      callback();
    }
    
    if (!useVelocity)
    {
      move();
    }
  }

  public void Delete()
  {
    Destroy(gameObject);
  }

  public virtual void move()
  {
    transform.Translate(Vector2.up * speed);
  }

  public void useCallback(callbackHandler callbackRequest)
  {
    callback = callbackRequest;
  }

  public virtual void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
      if (stateEffect!= null)
      {
        collision.gameObject.GetComponent<PlayerController>().stateMachine.AddState(stateEffect);
      }
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
