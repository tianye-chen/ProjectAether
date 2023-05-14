using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class StartMenuScript : MonoBehaviour
{


    public void StartGame()
    {
        SceneManager.LoadScene("intro-scene");
        UnityEngine.Debug.Log("Start");


    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void QuitGame()
    {

        UnityEngine.Debug.Log("Quit");
        Application.Quit();
    }

    public void back()
    {
        SceneManager.LoadScene("StartScreen");
    }


}
