using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    
    public static int level = 1;
    public static float minimumXP = 0;
    public static float maximumXP = 100;
    public static float currentXP = 0;
    public static int skillPoint = 0;
    public ProgressBar xpBar;

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown("space")) {
            currentXP += 20;
        } */
        if(currentXP >= maximumXP) {
            levelUP();
        }
        xpBar.UpdateValues(currentXP, maximumXP, minimumXP, level);

    }

    public void setCurrentXP(float xp) {
        currentXP += xp;
    }

    public void levelUP() {
        skillPoint += 1;
        minimumXP = maximumXP;
        maximumXP = Mathf.Round(maximumXP * (float)1.10);
            level += 1;

    }
}
