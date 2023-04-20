using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    public PauseMenuScript other;

    // Update is called once per frame
    public void back() {
        other.BacktoPauseMenu();
    }
}
