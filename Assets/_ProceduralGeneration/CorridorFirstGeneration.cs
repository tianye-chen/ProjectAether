using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CorridorFirstGeneration : RandomWalkMapGenerator
{
  [SerializeField]
  private int corridorLength = 15;
  [SerializeField]
  private int corridorCount = 5;
  [SerializeField] [Range(0.1f, 1)]
  private float roomPercent = 0.8f;
  [SerializeField]

  protected override void RunProceduralGeneration()
  {
    CorridorGeneration();
  }

  private void CorridorGeneration()
  {
    HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
    HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

    CreateCorridors(floorPositions, potentialRoomPositions);
    HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions); 
    floorPositions.UnionWith(roomPositions);
    
    tilemapVisualizer.paintFloorTiles(floorPositions);
    WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
  }

  private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
  {
    HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
    int numRoomsToCreate = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

    // Sort potentialRoomPositions by random order using Global Unique Identifier
    List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(numRoomsToCreate).ToList();

    foreach(var roomPos in roomsToCreate)
    {
      var roomFloor = RunRandomWalk(randomWalkData, roomPos);
      roomPositions.UnionWith(roomFloor);
    }

    return roomPositions;
  }

  private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
  {
    var currPos = startPos;
    potentialRoomPositions.Add(currPos);

    for (int i = 0; i < corridorCount; i++)
    {
      var corridor = ProceduralGeneration.RandomWalkCorridor(currPos, corridorLength);
      currPos = corridor[corridor.Count - 1];
      potentialRoomPositions.Add(currPos);
      floorPositions.UnionWith(corridor);
    }
  }
}
