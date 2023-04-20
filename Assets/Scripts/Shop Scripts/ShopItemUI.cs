using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    // Item Description Panel
    public TextMeshProUGUI costTxt;
    public TextMeshProUGUI descTxt;
    public Image image;

    public ShopItem item;
    ShopManager shopManager;

    public void Start()
    {
        this.GetComponent<Image>().sprite = item.icon;
    }

    public void OnPointerClick(BaseEventData eventData)
    {
        PointerEventData pointerData = (PointerEventData)eventData;
        if(pointerData.button == PointerEventData.InputButton.Left)
        {
            
        }
    }
}
