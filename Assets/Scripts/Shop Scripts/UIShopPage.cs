using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopPage : MonoBehaviour
{
    [SerializeField] private UIShopItems itemPrefab;
    [SerializeField] private RectTransform contentPanel;

    List<UIShopItems> itemsList = new List<UIShopItems>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeInven(int size)
    {
        for(int i = 0; i < size; i++)
        {
            UIShopItems uIShopItems = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uIShopItems.transform.SetParent(contentPanel);
            itemsList.Add(uIShopItems);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
