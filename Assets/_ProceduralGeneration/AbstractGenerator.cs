using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGenerator : MonoBehaviour
{
  [SerializeField]
  protected TilemapVisualizer tilemapVisualizer = null;
  [SerializeField]
  protected Vector2Int startPos = Vector2Int.zero;

  public void Generate()
  {
    tilemapVisualizer.ClearTiles();
    PlaceDecorations.ClearDecorations();
    RunProceduralGeneration();
  }

  protected abstract void RunProceduralGeneration();
}
