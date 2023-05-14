using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipmentStats : MonoBehaviour
{
    [SerializeField] private TMP_Text healthTxt, manaTxt, spdTxt, defTxt, atkTxt;
    private CharacterBase player;
    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        player = playerObject.GetComponent<CharacterBase>();
        UpdateEquipmentStats();
    }

    private void Update()
    {
        UpdateEquipmentStats();
    }

    public void UpdateEquipmentStats()
    {
        healthTxt.text = "HP: " + player.maxHealth.ToString();
        manaTxt.text = "Mana: " + player.maxMana.ToString();
        spdTxt.text = "SPD: " + player.maxSpeed.ToString();
        defTxt.text = "DEF: " + player.maxDef.ToString();
        atkTxt.text = "ATK: " + player.maxAtk.ToString();
    }
}
