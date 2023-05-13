using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // ITEM DATA
    public string itemName;
    public Sprite sprite;
    public bool isFull;
    public Sprite emptySprite;

    // SLOT
    [SerializeField] private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    // Equipped slots
    [SerializeField] private EquippedSlot headSlot, bodySlot, legSlot, weaponSlot;

    public ItemType itemType;

    private void Start()
    {
        inventoryManager = GameObject.Find("EquipmentCanvas").GetComponent<InventoryManager>();
    }

    public void AddItem(string itemName, Sprite sprite, ItemType itemType)
    {
        if (isFull) return;

        this.itemName = itemName;
        this.sprite = sprite;
        this.itemType = itemType;
        isFull = true;

        itemImage.sprite = sprite;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void EmptySlot()
    {
        this.itemImage.sprite = emptySprite;
        isFull = false;
    }

    private void EquipGear()
    {
        if(itemType == ItemType.head)
        {
            headSlot.EquipGear(itemImage.sprite, itemName);
        }
        if (itemType == ItemType.body)
        {
            bodySlot.EquipGear(itemImage.sprite, itemName);
        }
        if (itemType == ItemType.legs)
        {
            legSlot.EquipGear(itemImage.sprite, itemName);
        }
        if (itemType == ItemType.weapon)
        {
            weaponSlot.EquipGear(itemImage.sprite, itemName);
        }

        EmptySlot();
        Debug.Log("Emptied Slots");
    }

    public void OnLeftClick()
    {
        if (itemImage.sprite == null) return;

        if(thisItemSelected)
        {
            EquipGear();
            inventoryManager.DeselectAllSlots();
        }
        else
        {
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }

    public void OnRightClick()
    {
        if (itemImage.sprite == null) return;

        // Create new item
        GameObject itemToDrop = new GameObject(this.itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.setItemName(itemName);
        newItem.setSprite(itemImage.sprite);

        // Create and modify sprite renderer
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = this.itemImage.sprite;

        // Add collider
        itemToDrop.AddComponent<BoxCollider2D>();

        // Set location
        itemToDrop.transform.position = GameObject.Find("Player").transform.position;

        // empty slot
        EmptySlot();
    }
}
