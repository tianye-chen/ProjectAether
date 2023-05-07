using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // ITEM DATA
    public string itemName;
    public Sprite sprite;
    public bool isFull;

    // SLOT
    [SerializeField] private Image itemImage;

    public ItemType itemType;

    public void AddItem(string itemName, Sprite sprite, ItemType itemType)
    {
        if (isFull) return;

        this.itemName = itemName;
        this.sprite = sprite;
        this.itemType = itemType;
        isFull = true;

        itemImage.sprite = sprite;
    }
}
