using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class StatIncreaseUI : MonoBehaviour
{
    public TextMeshProUGUI HP;
    public TextMeshProUGUI ATK;
    public TextMeshProUGUI DEF;
    public TextMeshProUGUI MANA;
    public TextMeshProUGUI SPD;
    public TextMeshProUGUI ACC;
    public TextMeshProUGUI StatPointsText;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI healthValue;
    public int SP;
    public GameObject Player;
    public HealthBar healthBar;
    public HealthBar healthBar2;
    public ManaBar manaBar;
    
    

    // Start is called before the first frame update
    public void start() {
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        SP = Player.GetComponent<LevelSystem>().StatPoint;
        HP.text =  "HP: " + Player.GetComponent<CharacterBase>().maxHealth.ToString();
        ATK.text =  "ATK: " + Player.GetComponent<CharacterBase>().maxAtk.ToString();
        DEF.text =  "DEF: " + Player.GetComponent<CharacterBase>().maxDef.ToString();
        MANA.text =  "MANA: " + Player.GetComponent<CharacterBase>().maxMana.ToString();
        SPD.text =  "SPD: " + Player.GetComponent<CharacterBase>().maxSpeed.ToString();
        ACC.text =  "ACC: " + Player.GetComponent<CharacterBase>().maxAccuracy.ToString();
        StatPointsText.text = "STAT POINTS: " + Player.GetComponent<LevelSystem>().StatPoint.ToString();
        Level.text = "LEVEL: " + Player.GetComponent<LevelSystem>().level.ToString();
        healthValue.text = Player.GetComponent<CharacterBase>().health.ToString() + "/" + Player.GetComponent<CharacterBase>().maxHealth.ToString();
    }

   
    public void AddStat(string WhichStat) {
        
        if(SP > 0) {
            switch(WhichStat) {
                case "HP":
                    UnityEngine.Debug.Log("HP");
                    Player.GetComponent<CharacterBase>().maxHealth += 10;
                    Player.GetComponent<LevelSystem>().StatPoint -= 1;
                    healthBar.SetMaxHealth(Player.GetComponent<CharacterBase>().maxHealth);
                    healthBar.SetHealth(Player.GetComponent<CharacterBase>().health);
                    healthBar2.SetMaxHealth(Player.GetComponent<CharacterBase>().maxHealth);
                    healthBar2.SetHealth(Player.GetComponent<CharacterBase>().health);
                    Player.GetComponent<CharacterBase>().SavePlayer();
                    
                    break;
                case "ATK":
                    UnityEngine.Debug.Log("ATK");
                    Player.GetComponent<CharacterBase>().maxAtk += 1;
                    Player.GetComponent<CharacterBase>().atk = Player.GetComponent<CharacterBase>().maxAtk;
                    Player.GetComponent<LevelSystem>().StatPoint -= 1;
                    Player.GetComponent<CharacterBase>().SavePlayer();
                    break;
                case "DEF":
                    UnityEngine.Debug.Log("DEF");
                    Player.GetComponent<CharacterBase>().maxDef += 1;
                    Player.GetComponent<CharacterBase>().def = Player.GetComponent<CharacterBase>().maxDef;
                    Player.GetComponent<LevelSystem>().StatPoint -= 1;
                    Player.GetComponent<CharacterBase>().SavePlayer();
                    break;
                case "MANA":
                    UnityEngine.Debug.Log("MANA");
                    Player.GetComponent<CharacterBase>().maxMana += 10;
                    Player.GetComponent<LevelSystem>().StatPoint -=1;
                    manaBar.SetMaxMana(Player.GetComponent<CharacterBase>().maxMana);
                    manaBar.SetMana(Player.GetComponent<CharacterBase>().mana);
                    Player.GetComponent<CharacterBase>().SavePlayer();
                    break;
                case "SPD":
                    UnityEngine.Debug.Log("SPD");
                    Player.GetComponent<CharacterBase>().maxSpeed += 1;
                    Player.GetComponent<CharacterBase>().speed = Player.GetComponent<CharacterBase>().maxSpeed;
                    Player.GetComponent<LevelSystem>().StatPoint -= 1;
                    Player.GetComponent<CharacterBase>().SavePlayer();
                    break;
                case "ACC":
                    UnityEngine.Debug.Log("ACC");
                    Player.GetComponent<CharacterBase>().maxAccuracy += 1;
                    Player.GetComponent<CharacterBase>().accuracy = Player.GetComponent<CharacterBase>().maxAccuracy;
                    Player.GetComponent<LevelSystem>().StatPoint -= 1;
                    Player.GetComponent<CharacterBase>().SavePlayer();
                    break;

                    
            }
        }
        else {
            UnityEngine.Debug.Log("You don't have enough skill points");
        }
    }



}
