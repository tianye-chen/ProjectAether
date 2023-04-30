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
        HP.text =  "HP: " + Player.GetComponent<CharacterBase>().health.ToString();
        ATK.text =  "ATK: " + Player.GetComponent<CharacterBase>().atk.ToString();
        DEF.text =  "DEF: " + Player.GetComponent<CharacterBase>().def.ToString();
        MANA.text =  "MANA: " + Player.GetComponent<CharacterBase>().mana.ToString();
        SPD.text =  "SPD: " + Player.GetComponent<CharacterBase>().speed.ToString();
        ACC.text =  "ACC: " + Player.GetComponent<CharacterBase>().accuracy.ToString();
        StatPointsText.text = "STAT POINTS: " + Player.GetComponent<LevelSystem>().StatPoint.ToString();
    }

    public void AddStat(string WhichStat) {
        
        if(SP >= 0) {
            switch(WhichStat) {
                case "HP":
                    UnityEngine.Debug.Log("HP");
                    break;
                case "ATK":
                    UnityEngine.Debug.Log("ATK");
                    break;
                case "DEF":
                    UnityEngine.Debug.Log("DEF");
                    break;
                case "MANA":
                    UnityEngine.Debug.Log("MANA");
                    break;
                case "SPD":
                    UnityEngine.Debug.Log("SPD");
                    break;
                case "ACC":
                    UnityEngine.Debug.Log("ACC");
                    break;

                    
            }
        }
    }


}
