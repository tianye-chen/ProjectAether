using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
  [SerializeField]
  private Tilemap floorTilemap;
  [SerializeField]
  private TileBase floorTile;
  [SerializeField]
  private Tilemap wallTilemap;
  [SerializeField]
  private TileBase wallTopTile;

  public void paintFloorTiles(IEnumerable<Vector2Int> floorPos)
  {
    PaintFloorTiles(floorPos, floorTilemap, floorTile);
  }

  public void ClearTiles()
  {
    floorTilemap.ClearAllTiles();
    wallTilemap.ClearAllTiles();
  }

  private void PaintFloorTiles(IEnumerable<Vector2Int> floorPos, Tilemap floorTilemap, TileBase floorTile)
  {
    foreach (var pos in floorPos)
    {
      PaintTile(pos, floorTilemap, floorTile);
    }
  }

  internal void PaintWallTile(Vector2Int pos)
  {
    PaintTile(pos, wallTilemap, wallTopTile);
  }

  private void PaintTile(Vector2Int pos, Tilemap tilemap, TileBase tile)
  {
    var tilePos = tilemap.WorldToCell((Vector3Int)pos);
    tilemap.SetTile(tilePos, tile);
  }

}
