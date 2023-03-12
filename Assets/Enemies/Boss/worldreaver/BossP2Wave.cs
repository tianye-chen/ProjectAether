using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossP2Wave : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float speed = 10f;

    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (gameObject.name == "waveattack_1(Clone)")
            rigid.AddForce(-transform.up * speed);
        else
            transform.Translate(-Vector2.up * 3 / speed);

        if (transform.position.y < -100)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(1);
        }
        else if (collision.gameObject.tag == "Terrain")
            Destroy(gameObject);
    }
}
