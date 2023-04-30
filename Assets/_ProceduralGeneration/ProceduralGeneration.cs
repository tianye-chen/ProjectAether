using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGeneration
{
  public static HashSet<Vector2Int> SimpleRandomWalkAlgorithm(Vector2Int startPos, int walkLength){
    HashSet<Vector2Int> walkPath = new HashSet<Vector2Int>();
    walkPath.Add(startPos);
    var prevPos = startPos;

    for (int i = 0; i < walkLength; i++)
    {
      // Get a random direction and add it to the previous position to get the next position
      var nextPos = prevPos + Direction2D.GetRandomDirection();
      walkPath.Add(nextPos);
      prevPos = nextPos;
    }

    return walkPath;
  }

  public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength){
    List<Vector2Int> corridor = new List<Vector2Int>();
    var direction = Direction2D.GetRandomDirection();
    var currPos = startPos;

    corridor.Add(currPos);
    for (int i = 0; i < corridorLength; i++)
    {
      currPos += direction;
      corridor.Add(currPos);
    }

    return corridor;
  }
}