using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI text;
  [SerializeField]
  private RectTransform rectTransform;
  private float lifetime = 1f;
  private float timer = 0f;
  private float speed = 0.005f;
  private float damage;
  private float healing;

  void Start()
  {
    rectTransform.position = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), rectTransform.position.z);
  }

  void Update()
  {
    timer += Time.deltaTime;
    if (timer > lifetime)
    {
      Destroy(gameObject);
    }
    else
    {
      rectTransform.Translate(0, speed, 0);
    }
  }

  public void SetDamageNumber(float damage)
  {
    text.color = Color.red;
    this.damage = damage;
    text.text = "-"+damage.ToString();
  }

  public void SetHealingNumber(float healing)
  {
    text.color = Color.green;
    this.healing = healing;
    text.text = "+"+healing.ToString();
  }
}
