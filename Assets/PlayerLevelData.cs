using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerLevelData
{

  public float minimumXP, maximumXP, currentXP;
  public int level, StatPoint;

  public PlayerLevelData(LevelSystem l)
  {
    minimumXP = l.minimumXP;
    maximumXP = l.maximumXP;
    currentXP = l.currentXP;
    level = l.level;
    StatPoint = l.StatPoint;
  }

  private PlayerLevelData()
  {
    minimumXP = 0;
    maximumXP = 100;
    currentXP = 0;
    level = 1;
    StatPoint = 0;
  }

  public static PlayerLevelData GetNewPlayerLevelData()
  {
    return new PlayerLevelData();
  }
}
