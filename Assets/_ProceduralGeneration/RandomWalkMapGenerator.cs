using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkMapGenerator : MonoBehaviour
{
  public int walkLength = 10;
  public bool startRandomlyEachIteration = true;

  [SerializeField]
  protected Vector2Int startPos = Vector2Int.zero;
  [SerializeField]
  private int iterations = 1;

  public void runProceduralGeneration()
  {
    HashSet<Vector2Int> floorPos = RunRandomWalk();
    foreach (var pos in floorPos)
    {
      Debug.Log(pos);
    }
  }

  protected HashSet<Vector2Int> RunRandomWalk()
  {
    var currPos = startPos;
    HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();

    for (int i = 0; i < iterations; i++)
    {
      var path = ProceduralGeneationAlgorithm.SimpleRandomWalkAlgorithm(currPos, walkLength);

      // Union to ensure no duplicates
      floorPos.UnionWith(path);

      if (startRandomlyEachIteration)
      {
        currPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
      }
    }

    return floorPos;
  }
}
