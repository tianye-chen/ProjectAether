using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    // Item Description Panel
    public TextMeshProUGUI costInt;
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
        Debug.Log("SetData() was called");

        this.image.gameObject.SetActive(true);
        this.image.sprite = sprite;
        this.costInt.text = item.itemPrice.ToString();
        this.descTxt.text = item.itemDesc.ToString();

        Debug.Log("Changed Desc to: \"" + descTxt.text + "\"");
        Debug.Log("Set Cost to: " + costInt.text);
    }
}
