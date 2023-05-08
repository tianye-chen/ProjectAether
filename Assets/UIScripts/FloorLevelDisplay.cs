using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloorLevelDisplay : MonoBehaviour
{
    public TextMeshProUGUI floorLevelText;

    public void UpdateFloorLevelDisplay(int floorLevelText)
    {
        this.floorLevelText.text = "Floor " + floorLevelText.ToString();
    }
}
