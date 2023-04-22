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

}

public static class Direction2D
{
  public static List<Vector2Int> directionsList = new List<Vector2Int>()
  {
    Vector2Int.up,
    Vector2Int.right,
    Vector2Int.down,
    Vector2Int.left
  };

  public static Vector2Int GetRandomDirection()
  {
    return directionsList[Random.Range(0, directionsList.Count)];
  }
}
