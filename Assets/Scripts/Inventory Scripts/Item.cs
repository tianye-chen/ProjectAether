using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;

    private GameObject player;

    private InventoryManager inventoryManager;

    public ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("EquipmentCanvas").GetComponent<InventoryManager>();
        player = GameObject.FindWithTag("Player");
        Debug.Log(player.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision detected");
            inventoryManager.AddItem(itemName, sprite, itemType);
            Destroy(gameObject);
        }
    }

    public void setItemName(string itemName)
    {
        this.itemName = itemName;
    }

    public void setSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public string getItemName()
    {
        return this.itemName;
    }

    public Sprite getSprite()
    {
        return this.sprite;
    }
}
