using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    // SLOT APPEARANCE
    [SerializeField] private Image slotImage;
    //[SerializeField] private Image playerDisplayImage;

    // SLOT DATA
    [SerializeField] private ItemType itemType = new ItemType();

    private Sprite itemSprite;
    private string itemName;

    // OTHER VARS
    private bool slotInUse;
    [SerializeField] private bool thisItemSelected;
    [SerializeField] private Sprite emptySprite;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("EquipmentCanvas").GetComponent<InventoryManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // on left click
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
    }

    public void EquipGear(Sprite itemSprite, string itemName)
    {
        if (slotInUse) UnEquipGear();

        // Update image
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;

        // Update Data
        this.itemName = itemName;
        Debug.Log("itemName = " + itemName);

        slotInUse = true;
        Debug.Log("slotInUse = " + slotInUse);
    }

    public void OnLeftClick()
    {
        if(thisItemSelected && slotInUse)
        {
            UnEquipGear();
        }
        else
        {
            thisItemSelected = true;
        }
    }

    public void UnEquipGear()
    {
        inventoryManager.AddItem(itemName, itemSprite, itemType);

        this.itemSprite = emptySprite;
        slotImage.sprite = this.emptySprite;
        slotInUse = false;
    }
}
