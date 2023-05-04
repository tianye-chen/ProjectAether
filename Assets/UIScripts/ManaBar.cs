using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBar : MonoBehaviour 
{
    public Slider slider;
    public TextMeshProUGUI manaValue;
    public GameObject Player;

    public void SetMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void SetMana(float mana)
    {
        slider.value = mana;
    }
    void Update() {
        manaValue.text = Player.GetComponent<CharacterBase>().mana.ToString() + "/" + Player.GetComponent<CharacterBase>().maxMana.ToString();
    }
}
