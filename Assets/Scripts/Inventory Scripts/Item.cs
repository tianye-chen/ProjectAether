using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;

    private InventoryManager inventoryManager;

    public ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("EquipmentCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //inventoryManager.AddItem(itemName, sprite);
            Destroy(gameObject);
        }
    }
}
