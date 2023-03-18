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

    public GameObject pauseMenuUI;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }
    public void SkillsMenu()
    {
        UnityEngine.Debug.Log("Skills");
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
