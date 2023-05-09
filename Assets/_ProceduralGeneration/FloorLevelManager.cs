using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

  public static void PlacePlayer(HashSet<Vector2Int> floorPositions, GameObject player)
  {
    if (GameObject.FindGameObjectWithTag("Player") == null)
    {
      GameObject.Instantiate(player, new Vector3(0, 0, -2), Quaternion.identity);
    }

    if (floorLevel % 5 == 0)
    {
      Vector2Int pos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
      player.transform.position = new Vector3(pos.x, pos.y, -2);
    }
    else
    {
      player.transform.position = new Vector3(0, 0, -2);
    }
  }


  private void IncreaseFloorLevel()
  {
    floorLevel++;
    GameObject.FindGameObjectWithTag("UI_FloorDisplay").GetComponent<FloorLevelDisplay>().UpdateFloorLevelDisplay(floorLevel);
    generator.Generate();
    UpdateLevelManager();
    GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBase>().LoadPlayer();
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
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBase>().SavePlayer();
        IncreaseFloorLevel();
        Debug.Log("Floor level increased to " + floorLevel);
      }
    }
  }
}
