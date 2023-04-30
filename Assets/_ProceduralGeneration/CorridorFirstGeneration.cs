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
  [SerializeField]
  [Range(0.1f, 1)]
  private float roomPercent = 0.8f;
  [SerializeField]
  [Tooltip("Only works with odd numbers")]
  private float corridorWidth = 1;
  [SerializeField]
  private bool tunnelRooms = false;
  [SerializeField]
  private Sprite[] decorations, illuminatedDecorations;

  protected override void RunProceduralGeneration()
  {
    CorridorGeneration();
  }

  private void CorridorGeneration()
  {
    HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
    HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
    HashSet<Vector2Int> roomPositions;

    CreateCorridors(floorPositions, potentialRoomPositions);

    if (tunnelRooms)
    {
      roomPositions = createTunnelRooms(floorPositions);
    }
    else
    {
      roomPositions = CreateRooms(potentialRoomPositions);
      List<Vector2Int> deadEndPositions = FindDeadEnds(floorPositions);

      CreateRoomAtDeadEnd(deadEndPositions, roomPositions);
    }

    if (corridorWidth > 1)
    {
      floorPositions = widenCorridors(floorPositions, corridorWidth / 2);
    }

    floorPositions.UnionWith(roomPositions);
    PlaceDecorations.PlaceDecorationsOnFloor(floorPositions, decorations, illuminatedDecorations);
    tilemapVisualizer.paintFloorTiles(floorPositions);
    WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
  }

  // Widens the width of corridors, only works with odd numbers
  private HashSet<Vector2Int> widenCorridors(HashSet<Vector2Int> floorPositions, float corridorWidth)
  {
    HashSet<Vector2Int> widenedFloorPositions = new HashSet<Vector2Int>();

    // Given a single tile of a corridor, add tiles in each direction based on the corridor width
    foreach (var pos in floorPositions)
    {
      widenedFloorPositions.Add(pos);

      for (int i = 0; i < corridorWidth; i++)
      {
        foreach (var dir in Direction2D.directionsList)
        {
          widenedFloorPositions.Add(pos + dir * i);
        }
      }
    }

    return widenedFloorPositions;
  }

  private HashSet<Vector2Int> createTunnelRooms(HashSet<Vector2Int> potentialRoomPositions)
  {
    HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

    foreach (var pos in potentialRoomPositions)
    {
      var room = RunRandomWalk(randomWalkData, pos);
      floorPositions.UnionWith(room);
    }

    return floorPositions;
  }

  private void CreateRoomAtDeadEnd(List<Vector2Int> deadEndPositions, HashSet<Vector2Int> roomPositions)
  {
    // Create a room at each dead end position if it is not already a room
    foreach (var pos in deadEndPositions)
    {
      if (!roomPositions.Contains(pos))
      {
        var room = RunRandomWalk(randomWalkData, pos);
        roomPositions.UnionWith(room);
      }
    }
  }

  private List<Vector2Int> FindDeadEnds(HashSet<Vector2Int> floorPositions)
  {
    List<Vector2Int> deadEndPositions = new List<Vector2Int>();

    // Find dead ends by checking each floor position for neighbours, if it has only one neighbour it is a dead end
    foreach (var pos in floorPositions)
    {
      int neightbourCount = 0;

      foreach (var dir in Direction2D.directionsList)
      {
        if (floorPositions.Contains(pos + dir))
        {
          neightbourCount++;
        }
      }

      if (neightbourCount == 1)
      {
        deadEndPositions.Add(pos);
      }
    }

    return deadEndPositions;
  }

  private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
  {
    HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
    int numRoomsToCreate = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

    // Sort potentialRoomPositions by random order using Global Unique Identifier
    List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(numRoomsToCreate).ToList();

    foreach (var roomPos in roomsToCreate)
    {
      var roomFloor = RunRandomWalk(randomWalkData, roomPos);
      roomPositions.UnionWith(roomFloor);
    }

    return roomPositions;
  }

  private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
  {
    var currPos = startPos;

    // Only create rooms at corridors
    potentialRoomPositions.Add(currPos);

    // Create corridors using random walk algorithm 
    for (int i = 0; i < corridorCount; i++)
    {
      var corridor = ProceduralGeneration.RandomWalkCorridor(currPos, corridorLength);
      currPos = corridor[corridor.Count - 1];
      potentialRoomPositions.Add(currPos);
      floorPositions.UnionWith(corridor);
    }
  }
}
