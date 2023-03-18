using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

   
    public void ChangeVolume() //Function to change the volume.
    {
        AudioListener.volume= volumeSlider.value; //Volume equals to slider value
        Save(); //Saves volume
    }


    private void Load() //Loads previous saved volume
    {
        PlayerPrefs.GetFloat("musicVolume");
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume", volumeSlider.value);
    }
    private void Save() //Function to save volume 
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}
