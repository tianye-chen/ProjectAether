using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction2D
{
  public static List<Vector2Int> directionsList = new List<Vector2Int>()
  {
    Vector2Int.up,
    Vector2Int.right,
    Vector2Int.down,
    Vector2Int.left
  };

  public static List<Vector2Int> extendedDirectionsList = new List<Vector2Int>()
  {
    Vector2Int.up,
    Vector2Int.up + Vector2Int.right,
    Vector2Int.right,
    Vector2Int.right + Vector2Int.down,
    Vector2Int.down,
    Vector2Int.down + Vector2Int.left,
    Vector2Int.left,
    Vector2Int.left + Vector2Int.up
  };

  public static Vector2Int GetRandomDirection()
  {
    return directionsList[Random.Range(0, directionsList.Count)];
  }
}
