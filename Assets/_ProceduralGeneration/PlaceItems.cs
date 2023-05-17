using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaceItems
{
  private static List<ItemType> itemType = new List<ItemType> { ItemType.weapon, ItemType.head, ItemType.body, ItemType.legs };

  public static void PlaceItemsOnFloor(HashSet<Vector2Int> floorPositions, Item[] items, Sprite[] itemSprites)
  {
    foreach (var pos in floorPositions)
    {
      if (Random.value < 0.0005 && !WallGenerator.wallPos.Contains(pos))
      {
        Debug.Log("Item placed at: " + pos);
        PlaceItem(pos, items[Random.Range(0, items.Length)], itemSprites);
      }
    }
  }

  private static void PlaceItem(Vector2Int pos, Item item, Sprite[] itemSprites)
  {
    var itemInstance = Object.Instantiate(item, new Vector3(pos.x + 0.5f, pos.y + 0.5f - 2), Quaternion.identity);
    int randomNum = Random.Range(0, itemType.Count);

    Debug.Log("Item type: " + itemType[randomNum]);

    itemInstance.setItemType(itemType[randomNum]);
    itemInstance.setItemName(itemType[randomNum].ToString());
    itemInstance.setSprite(itemSprites[randomNum]);
    
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
