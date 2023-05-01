using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    
    public int level = 1;
    public static float minimumXP = 0;
    public static float maximumXP = 100;
    public static float currentXP = 0;
    public int StatPoint = 0;
    public ProgressBar xpBar;
    public ProgressBar xpBar2;

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown("space")) {
            currentXP += 1000;
        } */
        if(currentXP >= maximumXP) {
            levelUP();
        }
        xpBar.UpdateValues(currentXP, maximumXP, minimumXP, level);
        xpBar2.UpdateValues(currentXP, maximumXP, minimumXP, level);


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
