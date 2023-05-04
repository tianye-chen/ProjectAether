using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public float maxHealth, maxSpeed, maxAtk, maxDef, maxAccuracy, maxMana;
    public float health, speed, atk, def, accuracy, mana;
    public float minimumXP, maximumXP, currentXP;
    public int level;

    public PlayerData (CharacterBase c) {
        maxHealth = c.maxHealth;
        health = c.health;
        maxSpeed = c.maxSpeed;
        speed = c.speed;
        maxAtk = c.maxAtk;
        atk = c.atk;
        maxDef = c.maxDef;
        def = c.def;
        maxAccuracy = c.maxAccuracy;
        accuracy = c.accuracy;
        maxMana = c.maxMana;
        mana = c.mana;
        minimumXP = c.minimumXP;
        maximumXP = c.maximumXP;
        currentXP = c.currentXP;
        level = c.level;
        

    }

}
