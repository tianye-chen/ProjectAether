using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
  [SerializeField]
  private Tilemap levelTilemap;
  [SerializeField]
  private TileBase levelRuleTile;

  public void ClearTiles()
  {
    levelTilemap.ClearAllTiles();
  }

  private void PaintFloorTiles(IEnumerable<Vector2Int> floorPos, Tilemap floorTilemap, TileBase tile)
  {
    foreach (var pos in floorPos)
    {
      PaintTile(pos, floorTilemap, tile);
    }
  }

  internal void PaintWallTile(Vector2Int pos)
  {
    PaintTile(pos, levelTilemap, levelRuleTile);
  }

  public void paintFloorTiles(IEnumerable<Vector2Int> floorPos)
  {
    PaintFloorTiles(floorPos, levelTilemap, levelRuleTile);
  }

  private void PaintTile(Vector2Int pos, Tilemap tilemap, TileBase tile)
  {
    var tilePos = tilemap.WorldToCell((Vector3Int)pos);
    tilemap.SetTile(tilePos, tile);
  }

}
