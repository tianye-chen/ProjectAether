using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LostScreenController : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI levelReachedText;

  void Start()
  {
    levelReachedText.text = "Floor " + FloorLevelManager.getFloorLevel();
  }

  public void LoadGame()
  {
    SceneManager.LoadScene("generation");
    SaveSystem.ResetAllPlayerStats();
  }

  public void LoadHomeScreen()
  {
    SceneManager.LoadScene("StartScreen");
    SaveSystem.ResetAllPlayerStats();
  }
}
