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
    }

    private PlayerData()
    {
      maxHealth = 1000f;
      health = 1000f;
      maxSpeed = 5f;
      speed = 5f;
      maxAtk = 10f;
      atk = 10f;
      maxDef = 10f;
      def = 10f;
      maxAccuracy  = 10f;
      accuracy = 10f;
      maxMana = 100f;
      mana = 100f;
    }

    public static PlayerData GetNewPlayer()
    {
      return new PlayerData();
    } 
}
