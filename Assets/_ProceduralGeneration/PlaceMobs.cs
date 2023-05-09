using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaceMobs
{
  public static void PlaceMobsOnFloor(HashSet<Vector2Int> floorPositions, EnemyBase[] mobsArray)
  {
    bool hasBossBeenPlaced = false;

    foreach (var pos in floorPositions)
    {
      // if pos is not within 2 tiles of a wall and not within 2 tiles of another mob
      if (!WallGenerator.wallPos.Contains(pos) && !Physics2D.OverlapCircle(pos, 5f, LayerMask.GetMask("Enemy")))
      {
        if (Random.value < (FloorLevelManager.floorLevel % 5 == 0 ? 0.001f : 0.0025f * ((float)FloorLevelManager.floorLevel  / 2)))
        {
          if (!hasBossBeenPlaced && FloorLevelManager.floorLevel % 5 == 0)
          {
            PlaceBoss(mobsArray[Random.Range(mobsArray.Length - 2, mobsArray.Length)]);
            hasBossBeenPlaced = true;
          }

          if (mobsArray.Length == 0) continue;
          PlaceMob(pos, mobsArray[Random.Range(0, mobsArray.Length - 2)]);
        }
      }
    }
  }

  private static void PlaceMob(Vector2Int pos, EnemyBase mob)
  {
    var mobInstance = Object.Instantiate(mob, new Vector3(pos.x + 0.5f, pos.y + 0.5f, -2), Quaternion.identity);
    mobInstance.tag = "Enemy";
    mobInstance.transform.SetParent(GameObject.Find("EnemyContainer").transform);
  }

  private static void PlaceBoss(EnemyBase boss){
    var bossInstance = Object.Instantiate(boss, new Vector3(0.5f, 0.5f, -2), Quaternion.identity);
    bossInstance.tag = "Enemy";
    bossInstance.transform.SetParent(GameObject.Find("EnemyContainer").transform);
  }

  public static void ClearMobs()
  {
    foreach (var mob in GameObject.FindGameObjectsWithTag("Enemy"))
    {
      if (Application.isPlaying)
      {
        Object.Destroy(mob);
      }
      else
      {
        Object.DestroyImmediate(mob);
      }
    }
  }
}
