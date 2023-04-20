using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    public ShopItemUI itemPrefab;
    public RectTransform itemContainer;
    public TextMeshProUGUI goldTxt;

    public Button buyBtn;

    public List<ShopItemUI> itemList;

    int gold = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGoldText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGoldText()
    {
        goldTxt.text = "Cost: " + gold.ToString();
    }
}
