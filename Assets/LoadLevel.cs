using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadGame", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("generation");
    }
}
