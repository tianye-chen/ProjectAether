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
    public TextMeshProUGUI healCooldown;
    public float electricValue;
    public float fireValue;
    public float waterValue;
    public float windValue;
    public float earthValue;
    public float healValue;
    public int selectedElement;
    public Healing_Ability Healing_Ability;
    public Elemental_Attack Elemental_Attack;
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
        electricValue = Elemental_Attack.GetRemainingCooldowns("lightning");
        electricCooldown.text = electricValue.ToString("#");
        fireValue = Elemental_Attack.GetRemainingCooldowns("fire");
        fireCooldown.text = fireValue.ToString("#");
        waterValue = Elemental_Attack.GetRemainingCooldowns("water");
        waterCooldown.text = waterValue.ToString("#");
        windValue = Elemental_Attack.GetRemainingCooldowns("air");
        windCooldown.text = windValue.ToString("#");
        earthValue = Elemental_Attack.GetRemainingCooldowns("earth"); 
        earthCooldown.text = earthValue.ToString("#");
        selectedElement = Elemental_Attack.GetSelectedElement();
        healValue = Healing_Ability.GetRemainingCooldown();
        healCooldown.text = healValue.ToString("#");
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
