using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public static bool IsPaused = false;
    private static bool isActive = false;
    public GameObject pauseMenuUI;
    public GameObject SkillsMenuUI;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPaused == true && isActive == true)
            {
                Resume();
            }
            else if(IsPaused == false)
            {
                Pause();
            }
            else
            {
                BacktoPauseMenu();
            }
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false; 
        isActive = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        isActive = true;
    }
    public void SkillsMenu()
    {
        isActive = false;
        pauseMenuUI.SetActive(false);
        UnityEngine.Debug.Log("Skills");
        SkillsMenuUI.SetActive(true);
        
    }
    public void BacktoPauseMenu()
    {
        SkillsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        isActive = true;

    }

    public void OptionsMenu()
    {
        UnityEngine.Debug.Log("Options");
    }

    public void QuitGame()
    {

        UnityEngine.Debug.Log("Quit");
        SceneManager.LoadScene("StartScreen");
        Resume();
    }
}
