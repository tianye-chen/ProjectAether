using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkMapGenerator : MonoBehaviour
{
  public bool startRandomlyEachIteration = true;
  public int walkLength = 10;

  [SerializeField]
  protected Vector2Int startPos = Vector2Int.zero;
  [SerializeField]
  private int iterations = 1;
  [SerializeField]
  private TilemapVisualizer tilemapVisualizer;

  public void runProceduralGeneration()
  {

    // Collection of all floor positions
    HashSet<Vector2Int> floorPos = RunRandomWalk();
    tilemapVisualizer.ClearTiles();
    tilemapVisualizer.paintFloorTiles(floorPos);
  }

  protected HashSet<Vector2Int> RunRandomWalk()
  {
    var currPos = startPos;
    HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();

    for (int i = 0; i < iterations; i++)
    {
      var path = ProceduralGenerationAlgorithm.SimpleRandomWalkAlgorithm(currPos, walkLength);

      // Union to ensure no duplicates
      floorPos.UnionWith(path);

      if (startRandomlyEachIteration)
      {

        // Get a random position from floorPos
        currPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
      }
    }

    return floorPos;
  }
}
