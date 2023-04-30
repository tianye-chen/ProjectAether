using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkMapGenerator : AbstractGenerator
{
  [SerializeField]
  protected RandomWalkData randomWalkData;

  protected override void RunProceduralGeneration()
  {

    // A collection of all floor positions
    HashSet<Vector2Int> floorPos = RunRandomWalk(randomWalkData, startPos);
    tilemapVisualizer.ClearTiles();
    tilemapVisualizer.paintFloorTiles(floorPos);
    WallGenerator.CreateWalls(floorPos, tilemapVisualizer);
  }

  protected HashSet<Vector2Int> RunRandomWalk(RandomWalkData randomWalkData, Vector2Int pos)
  {
    var currPos = pos;
    HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();

    for (int i = 0; i < randomWalkData.iterations; i++)
    {
      var path = ProceduralGeneration.SimpleRandomWalkAlgorithm(currPos, randomWalkData.walkLength);

      // Union to ensure no duplicates
      floorPos.UnionWith(path);

      if (randomWalkData.startRandomlyEachIteration)
      {

        // Get a random position from floorPos
        currPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
      }
    }

    return floorPos;
  }
}
