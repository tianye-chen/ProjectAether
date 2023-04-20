using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    public ShopItemUI itemUI;
    public RectTransform itemContainer;
    public TextMeshProUGUI goldTxt;

    public Button buyBtn;

    //public List<ShopItemUI> itemList;

    public int gold = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGoldText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem()
    {
        if(gold >= itemUI.item.itemPrice)
        {
            gold -= itemUI.item.itemPrice;
        }
    }

    public void UpdateGoldText()
    {
        goldTxt.text = "Cost: " + gold.ToString();
    }
}
