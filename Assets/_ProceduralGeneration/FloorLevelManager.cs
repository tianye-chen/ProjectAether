using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLevelManager : MonoBehaviour
{
  public static int floorLevel = 1;
  [SerializeField]
  private Sprite closedSprite, openSprite;
  private bool isOpen = false;
  private AbstractGenerator generator;
  private int enemyCount = 0;


  private void Start()
  {
    generator = GetComponentInChildren<AbstractGenerator>();
    GetComponent<SpriteRenderer>().sprite = closedSprite;
    enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
  }

  private void Update()
  {
    int currEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

    if (currEnemyCount < enemyCount)
    {
      enemyCount = currEnemyCount;
      if (enemyCount == 0)
      {
        GetComponent<SpriteRenderer>().sprite = openSprite;
        isOpen = true;
      }
    }
  }

  private void IncreaseFloorLevel()
  {
    floorLevel++;
    GameObject.FindGameObjectWithTag("UI_FloorDisplay").GetComponent<FloorLevelDisplay>().UpdateFloorLevelDisplay(floorLevel);
    generator.Generate();
    UpdateLevelManager();
  }

  private void UpdateLevelManager()
  {
    enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    GetComponent<SpriteRenderer>().sprite = closedSprite;
    isOpen = false;
  }

  public void OnTriggerEnter2D(Collider2D collision)
  {
    if (isOpen)
    {
      if (collision.gameObject.tag == "Player")
      {
        IncreaseFloorLevel();
        Debug.Log("Floor level increased to " + floorLevel);
      }
    }
  }
}
