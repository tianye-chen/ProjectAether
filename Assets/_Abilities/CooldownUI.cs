using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CooldownUI : MonoBehaviour
{
    public TextMeshProUGUI electricCooldown;
    public TextMeshProUGUI fireCooldown;
    public TextMeshProUGUI waterCooldown;
    public TextMeshProUGUI windCooldown;
    public TextMeshProUGUI earthCooldown;
    public float electricValue;
    public float fireValue;
    public float waterValue;
    public float windValue;
    public float earthValue;
    public int selectedElement;
    public GameObject s1;
    public GameObject s2;
    public GameObject s3;
    public GameObject s4;
    public GameObject s5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
        UpdateSelector();
    }
        
        

    void UpdateValues() {
        electricValue = Elemental_Attack.cooldownTimers[0];
        electricCooldown.text = electricValue.ToString("#");
        fireValue = Elemental_Attack.cooldownTimers[1];
        fireCooldown.text = fireValue.ToString("#");
        waterValue = Elemental_Attack.cooldownTimers[2];
        waterCooldown.text = waterValue.ToString("#");
        windValue = Elemental_Attack.cooldownTimers[3];
        windCooldown.text = windValue.ToString("#");
        earthValue = Elemental_Attack.cooldownTimers[4]; 
        earthCooldown.text = earthValue.ToString("#");
        selectedElement = Elemental_Attack.selectedElement;
    }

    void UpdateSelector() {
        switch(selectedElement)
        {
            case 0:
                s1.SetActive(true);
                s2.SetActive(false);
                s3.SetActive(false);
                s4.SetActive(false);
                s5.SetActive(false);
                
                break;
            case 1:
                s2.SetActive(true);
                s1.SetActive(false);
                s3.SetActive(false);
                s4.SetActive(false);
                s5.SetActive(false);
                break;
            case 2:
                s3.SetActive(true);
                s1.SetActive(false);
                s2.SetActive(false);
                s4.SetActive(false);
                s5.SetActive(false);
                break;
            case 3:
                s4.SetActive(true);
                s1.SetActive(false);
                s2.SetActive(false);
                s3.SetActive(false);
                s5.SetActive(false);
                break;
            case 4:
                s5.SetActive(true);
                s1.SetActive(false);
                s2.SetActive(false);
                s3.SetActive(false);
                s4.SetActive(false);
                break;
        }
    }
    
}
