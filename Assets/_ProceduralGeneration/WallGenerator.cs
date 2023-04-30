using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
  public static HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();

  public static void CreateWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer tilemapVisualizer)
  {
    wallPos = FindWallsInDirections(floorPos, Direction2D.extendedDirectionsList);

    foreach (var pos in wallPos)
    {
      tilemapVisualizer.PaintWallTile(pos);
    }
  }

  private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPos, List<Vector2Int> directionsList)
  {
    HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();

    foreach (var pos in floorPos)
    {
      foreach (var dir in directionsList)
      {
        var neighborPos = pos + dir;
        if (!floorPos.Contains(neighborPos))
        {
          wallPos.Add(neighborPos);
        }
      }
    }

    return wallPos;
  }
}
