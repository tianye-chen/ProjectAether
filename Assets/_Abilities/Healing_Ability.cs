using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Healing_Ability", menuName = "Ability/Healing_Ability")]
public class Healing_Ability : Ability
{
  public override void Activate(GameObject parent)
  {
    Debug.Log("Healing Ability Activated");
  }
}
