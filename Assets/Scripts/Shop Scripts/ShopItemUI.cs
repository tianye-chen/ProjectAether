using System;
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

    public void Start()
    {
        this.GetComponent<Image>().sprite = item.icon;
    }

    public void OnPointerClick(BaseEventData eventData)
    {
        PointerEventData pointerData = (PointerEventData)eventData;
        if(pointerData.button == PointerEventData.InputButton.Left)
        {
            SetData(item.icon);
        }
    }

    public void SetData(Sprite sprite)
    {
        this.image.gameObject.SetActive(true);
        this.image.sprite = sprite;
        this.costTxt.text = item.itemPrice.ToString();
        this.descTxt.text = item.itemDesc.ToString();
    }
}
