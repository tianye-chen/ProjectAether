using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossP2Wave : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rigid;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if ((collision.gameObject.tag == "Terrain" && gameObject.transform.position.y < 5) || (gameObject.name == "waveattack_2(Clone)" && collision.gameObject.tag == "Terrain"))
            StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1F);
        Destroy(gameObject);
    }
}
