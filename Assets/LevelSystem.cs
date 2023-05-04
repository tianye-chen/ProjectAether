using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    
    public int level = 1;
    public float minimumXP = 0;
    public float maximumXP = 100;
    public float currentXP = 0;
    public int StatPoint = 0;
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
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")) {
            currentXP += 1000;
        }
        if(currentXP >= maximumXP) {
            levelUP();
        }
        xpBar.UpdateValues(currentXP, maximumXP, minimumXP, level);
        xpBar2.UpdateValues(currentXP, maximumXP, minimumXP, level);
        Player.GetComponent<CharacterBase>().UpdateLevel(minimumXP, maximumXP, currentXP, level);



    }

    public void setCurrentXP(float xp) {
        currentXP += xp;
    }

    public void levelUP() {
        StatPoint += 1;
        minimumXP = maximumXP;
        maximumXP = Mathf.Round(maximumXP * (float)1.10);
            level += 1;

    }
}
