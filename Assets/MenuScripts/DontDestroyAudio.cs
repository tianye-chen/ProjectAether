using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyAudio : MonoBehaviour
{
    public static DontDestroyAudio Instance;
    void Awake()
    {
        if (Instance == null) //If there is no instance of audio then do not destroy
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
            Destroy(gameObject); 
    }
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene(); //Checks for current scene
        if (scene.name == "SampleScene") //If scene Is sample scene then destory mainmenu background audio
        {
            Destroy(gameObject);
        }
    }
}
