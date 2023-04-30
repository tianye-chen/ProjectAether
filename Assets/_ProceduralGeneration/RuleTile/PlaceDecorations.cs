using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaceDecorations
{
    public static void PlaceDecorationsOnFloor(HashSet<Vector2Int> floorPositions, Sprite[] decorations, Sprite[] illuminatedDecorations)
    {
        foreach (var pos in floorPositions)
        {
            if (Random.value < 0.05f && !WallGenerator.wallPos.Contains(pos))
            {
                if (Random.value < 0.75f)
                {
                  if (decorations.Length == 0) continue;
                  PlaceDecoration(pos, decorations[Random.Range(0, decorations.Length)]);
                }
                else
                {
                  if (illuminatedDecorations.Length == 0) continue;
                  PlaceIlluminatedDecoration(pos, illuminatedDecorations[Random.Range(0, illuminatedDecorations.Length)]);
                }
            }
        }
    }

  private static void PlaceIlluminatedDecoration(Vector2Int pos, Sprite sprite)
  {
    var decoration = new GameObject("Illuminated Decoration", typeof(SpriteRenderer));
    decoration.tag = "Decoration";

    decoration.transform.position = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
    decoration.GetComponent<SpriteRenderer>().sprite = sprite;
    decoration.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
  }

  private static void PlaceDecoration(Vector2Int pos, Sprite sprite)
    {
        var decoration = new GameObject("Decoration", typeof(SpriteRenderer));
        decoration.tag = "Decoration";

        // Place decoration and adjust to be aligned with tilemap grid
        decoration.transform.position = new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
        decoration.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public static void ClearDecorations()
    {
        foreach (var decoration in GameObject.FindGameObjectsWithTag("Decoration"))
        {
            Object.DestroyImmediate(decoration);
        }
    }
}
