using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    private float verticalMovement;
    private float horizontalMovement;

    private int selectedAbilityIndex = 0;

    [SerializeField]
    private List<Ability> abilities; 


    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        ScrollAbilities();
        if(Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(abilities[selectedAbilityIndex].Use(this));
        }
    }

    void FixedUpdate()
    {
        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");

        // Flip sprite if moving left
        if (horizontalMovement < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else {
            transform.localScale = new Vector3(1, 1, 1);
        }

        rigid.velocity = new Vector2(horizontalMovement * speed, verticalMovement * speed);
    }

    public void TakeDamage(float damage){
        Debug.Log("TakeDamage");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(1);

            Debug.Log("colliding");
        }
    }
    private void ScrollAbilities()
    {
        if (abilities.Count == 0) return;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedAbilityIndex++;
            if(selectedAbilityIndex == abilities.Count)
            {
                selectedAbilityIndex = 0;
            }
        }
    }
}
