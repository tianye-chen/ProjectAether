using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "ElementalAttack", menuName = "Ability/ElementalAttack")]
public class Elemental_Attack : Ability
{
  public enum element { Water = 1, Fire = 2, Wind = 3, Earth = 4, Electricity = 5};
   
  public override void Activate(GameObject parent)
  {
    throw new System.NotImplementedException();
  }
}