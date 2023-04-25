using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour

{
    public int level = 1;
    public TextMeshProUGUI xpText;
    public float minimumXP;
    public float maximumXP;
    public float currentXP;
    public Slider slider;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")) {
            currentXP += 20;
        }
        GetCurrentFill();
        xpText.text = "XP: " + currentXP.ToString() + "/" + maximumXP.ToString();
    }

    void GetCurrentFill() {
        float currentOffset = currentXP - minimumXP;
        float maximumOffset = maximumXP - minimumXP;
        float fillAmount = currentOffset / maximumOffset;
        slider.value = fillAmount;
        if(currentXP >= maximumXP) {
            minimumXP = maximumXP;
            maximumXP = Mathf.Round(maximumXP * (float)1.5);
            level += 1;
        }
    }


}
