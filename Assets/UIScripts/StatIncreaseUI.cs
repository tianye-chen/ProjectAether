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
    public int SP;
    public GameObject Player;

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
    }


    public void AddStat(string WhichStat) {
        
        if(SP > 0) {
            switch(WhichStat) {
                case "HP":
                    UnityEngine.Debug.Log("HP");
                    Player.GetComponent<CharacterBase>().maxHealth += 10;
                    Player.GetComponent<LevelSystem>().StatPoint -= 1;
                    break;
                case "ATK":
                    UnityEngine.Debug.Log("ATK");
                    Player.GetComponent<CharacterBase>().maxAtk += 5;
                    Player.GetComponent<CharacterBase>().atk = Player.GetComponent<CharacterBase>().maxAtk;
                    Player.GetComponent<LevelSystem>().StatPoint -= 1;
                    break;
                case "DEF":
                    UnityEngine.Debug.Log("DEF");
                    Player.GetComponent<CharacterBase>().maxDef += 5;
                    Player.GetComponent<CharacterBase>().def = Player.GetComponent<CharacterBase>().maxDef;
                    Player.GetComponent<LevelSystem>().StatPoint -= 1;
                    break;
                case "MANA":
                    UnityEngine.Debug.Log("MANA");
                    Player.GetComponent<CharacterBase>().maxMana += 10;
                    Player.GetComponent<LevelSystem>().StatPoint -=1;
                    break;
                case "SPD":
                    UnityEngine.Debug.Log("SPD");
                    Player.GetComponent<CharacterBase>().maxSpeed += 5;
                    Player.GetComponent<CharacterBase>().speed = Player.GetComponent<CharacterBase>().maxSpeed;
                    Player.GetComponent<LevelSystem>().StatPoint -= 1;
                    break;
                case "ACC":
                    UnityEngine.Debug.Log("ACC");
                    Player.GetComponent<CharacterBase>().maxAccuracy += 5;
                    Player.GetComponent<CharacterBase>().accuracy = Player.GetComponent<CharacterBase>().maxAccuracy;
                    Player.GetComponent<LevelSystem>().StatPoint -= 1;
                    break;

                    
            }
        }
        else {
            UnityEngine.Debug.Log("You don't have enough skill points");
        }
    }



}
