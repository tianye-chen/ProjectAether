using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaceItems
{
  public static void PlaceItemsOnFloor(HashSet<Vector2Int> floorPositions, Item[] items)
  {
    foreach (var pos in floorPositions)
    {
      if (Random.value > 0.001 && !WallGenerator.wallPos.Contains(pos))
      {

      }
    }
  }

  private static void PlaceItem(Vector2Int pos, Item item)
  {
    var itemInstance = Object.Instantiate(item, new Vector3(pos.x + 0.5f, pos.y + 0.5f - 2), Quaternion.identity);
    itemInstance.tag = "Item";
    itemInstance.transform.SetParent(GameObject.Find("ItemContainer").transform);
  }

  public static void ClearItems()
  {
    foreach (var item in GameObject.FindGameObjectsWithTag("Item"))
    {
      if (Application.isPlaying)
      {
        Object.Destroy(item);
      }
      else
      {
        Object.DestroyImmediate(item);
      }
    }
  }
}
