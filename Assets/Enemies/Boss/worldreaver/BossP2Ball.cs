using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossP2Ball : MonoBehaviour
{
    [SerializeField] public float FireRate = 1f;
    [SerializeField] public GameObject DefaultBullet;
    [SerializeField] public Rigidbody2D rigid;
    private GameObject InstBullet;
    private bool IsAttack = false;
    private bool IsOverBoundary = false;
    private float speed = 10;
    private float timer = 0f;
    private float TimeSinceInstanced = 0;

    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!IsAttack)
        {
            transform.RotateAround(new Vector3(0,6), new Vector3(0,0,1f), Time.deltaTime /2 * 90f);
        }
        if (timer > FireRate && !IsOverBoundary) // Prevents firing when the object is outside the boundary
        {
            gameObject.GetComponent<AudioSource>().Play();
            for (float i = 0; i <= 360; i += 45)
            {
                InstBullet = Instantiate(DefaultBullet, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                InstBullet.GetComponent<BulletController>().SetInstObject("Boss");
                InstBullet.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                InstBullet.GetComponent<BoxCollider2D>().size = new Vector2(0.2498093f, 0.2494715f);
                InstBullet.GetComponent<BoxCollider2D>().offset = new Vector2(-0.005095348f, 0.004776936f);
                InstBullet.transform.eulerAngles = Vector3.forward * i;
            }
            timer = 0;
        }
        else
            timer += Time.deltaTime;
        if (IsAttack) // If the current instance is part of an attack of phase 2 boss
        {
            transform.Translate(Vector2.up * 0.5f / speed);
            TimeSinceInstanced += Time.deltaTime;
            if (TimeSinceInstanced >= 5)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundary")
            if (IsOverBoundary)
                StartCoroutine(Wait());
            else
                IsOverBoundary = !IsOverBoundary;
    }

    public void SetIsAttack(bool val) 
    {
        IsAttack = val;
    }

    IEnumerator Wait() 
    {
        yield return new WaitForSeconds(0.3f);
        IsOverBoundary = !IsOverBoundary;
    }
}
