using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int attack, defense, speed, health, mana;

    public void EquipItem()
    {
        // Update stats
        CharacterBase player = GameObject.Find("StatManager").GetComponent<CharacterBase>();
        player.maxAtk += attack;
        player.maxDef += defense;
        player.maxSpeed += speed;
        player.maxHealth += health;
        player.maxMana += mana;
    }

    public void UnEquipItem()
    {
        // Update stats
        CharacterBase player = GameObject.Find("StatManager").GetComponent<CharacterBase>();
        player.maxAtk -= attack;
        player.maxDef -= defense;
        player.maxSpeed -= speed;
        player.maxHealth -= health;
        player.maxMana -= mana;
    }

    public enum StatToChange
    {
        none,
        health,
        mana,
    };

    public enum Attribute
    {
        none,
        strength,
        defense,
    };
}
