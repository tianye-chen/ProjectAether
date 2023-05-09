using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    
    public int level;
    public float minimumXP;
    public float maximumXP;
    public float currentXP;
    public int StatPoint;
    public ProgressBar xpBar;
    public ProgressBar xpBar2;
    public GameObject Player;

    public void Start() {
        if(Player == null) {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        if(xpBar == null) {
            xpBar = GameObject.Find("XPBar").GetComponent<ProgressBar>();
        }
        if(xpBar2 == null) {
            xpBar2 = GameObject.Find("StatusMenu").transform.GetChild(1).GetChild(16).GetComponent<ProgressBar>();
        }
        LoadPlayerLevel();
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")) {
            resetLevel();
        }
        
        if(currentXP >= maximumXP) {
            levelUP();
        }
        xpBar.UpdateValues(currentXP, maximumXP, minimumXP, level);
        xpBar2.UpdateValues(currentXP, maximumXP, minimumXP, level);
        //Player.GetComponent<CharacterBase>().UpdateLevel(minimumXP, maximumXP, currentXP, level);



    }

    public void setCurrentXP(float xp) {
        currentXP += xp;
        SavePlayerLevel();
    }

    public void levelUP() {
        StatPoint += 1;
        minimumXP = maximumXP;
        maximumXP = Mathf.Round(maximumXP * (float)1.10);
        level += 1;
        SavePlayerLevel();
        

    }

    public void SavePlayerLevel() {
      SaveSystem.SavePlayerLevel(this);
    }


    public void LoadPlayerLevel() {
      PlayerLevelData data2 = SaveSystem.LoadPlayerLevel();
      minimumXP = data2.minimumXP;
      maximumXP = data2.maximumXP;
      currentXP = data2.currentXP;
      level = data2.level;
      StatPoint = data2.StatPoint;

    }
    public void resetLevel() {
        minimumXP = 0;
        maximumXP = 100;
        currentXP = 0;
        level = 1;
        StatPoint = 0;
    }
}
