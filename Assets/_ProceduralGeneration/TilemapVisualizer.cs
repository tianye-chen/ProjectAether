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

  public void paintFloorTiles(IEnumerable<Vector2Int> floorPos)
  {
    PaintFloorTiles(floorPos, floorTilemap, floorTile);
  }

  public void ClearTiles()
  {
    floorTilemap.ClearAllTiles();
  }

  private void PaintFloorTiles(IEnumerable<Vector2Int> floorPos, Tilemap floorTilemap, TileBase floorTile)
  {
    foreach (var pos in floorPos)
    {
      PaintTile(pos, floorTilemap, floorTile);
    }
  }

  private void PaintTile(Vector2Int pos, Tilemap tilemap, TileBase tile)
  {
    var tilePos = tilemap.WorldToCell((Vector3Int)pos);
    tilemap.SetTile(tilePos, tile);
  }
}
