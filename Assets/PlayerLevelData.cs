using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerLevelData
{

   public float minimumXP, maximumXP, currentXP;
   public int level, StatPoint;

   public PlayerLevelData(LevelSystem l) {
        minimumXP = l.minimumXP;
        maximumXP = l.maximumXP;
        currentXP = l.currentXP;
        level = l.level;
        StatPoint = l.StatPoint;
   }
}
