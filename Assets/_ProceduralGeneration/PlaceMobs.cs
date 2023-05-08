using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaceMobs
{
  public static void PlaceMobsOnFloor(HashSet<Vector2Int> floorPositions, EnemyBase[] mobsArray)
  {
    foreach (var pos in floorPositions)
    {
      // if pos is not within 2 tiles of a wall and not within 2 tiles of another mob
      if (!WallGenerator.wallPos.Contains(pos) && !Physics2D.OverlapCircle(pos, 5f, LayerMask.GetMask("Enemy")))
      {
        if (Random.value < 0.01f)
        {
          if (mobsArray.Length == 0) continue;
          PlaceMob(pos, mobsArray[Random.Range(0, mobsArray.Length)]);
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
