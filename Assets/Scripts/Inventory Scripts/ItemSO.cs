using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public Attribute attributeToChange = new Attribute();
    public int amountToChangeAttr;

    private HealthBar healthBar;
    private ManaBar manaBar;
    private CharacterBase character;

    public void UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            healthBar.SetMaxHealth(amountToChangeStat);
        }
        if (statToChange == StatToChange.mana)
        {
            manaBar.SetMaxMana(amountToChangeStat);
        }
        if(attributeToChange == Attribute.strength)
        {
            character.maxAtk = amountToChangeAttr;
        }
        if (attributeToChange == Attribute.defense)
        {
            character.maxDef = amountToChangeAttr;
        }
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
