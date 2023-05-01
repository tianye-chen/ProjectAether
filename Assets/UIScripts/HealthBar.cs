using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthValue;
    public GameObject Player;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    void Update() {
        healthValue.text = Player.GetComponent<CharacterBase>().health.ToString() + "/" + Player.GetComponent<CharacterBase>().maxHealth.ToString();
    }

}
