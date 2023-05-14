using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    // shop canvas
    public GameObject shopCanvas;

    public TextMeshProUGUI cost;
    public RectTransform itemContainer;
    public TextMeshProUGUI goldTxt;

    public int gold = 0;
    private bool isHidden;

    // Start is called before the first frame update
    void Start()
    {
        isHidden = false;
        UpdateGoldText();
    }

    // Update is called once per frame
    void Update()
    {
        // show shop canvas
        if(Input.GetKeyDown(KeyCode.P) && isHidden == true)
        {
            shopCanvas.SetActive(true);
        }
    }

    public void BuyItem()
    {
        Debug.Log("BuyItem() was called\nSubtracting: " + int.Parse(cost.text));


        if (gold >= int.Parse(cost.text))
        {
            gold -= int.Parse(cost.text);
            UpdateGoldText();
        }
    }

    public void UpdateGoldText()
    {
        goldTxt.text = gold.ToString();
    }

    public void ExitGUI()
    {
        isHidden = true;
        shopCanvas.SetActive(false);
    }
}
