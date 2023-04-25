using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour

{
    public float minimumXP;
    public float maximumXP;
    public float currentXP;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill() {
        float currentOffset = currentXP - minimumXP;
        float maximumOffset = maximumXP - minimumXP;
        float fillAmount = currentOffset / maximumOffset;
        slider.value = fillAmount;
    }


}
