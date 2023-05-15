using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityManager : MonoBehaviour
{
  [SerializeField]
  private IDictionary<string, Ability> abilities = new Dictionary<string, Ability>();

  public void Start()
  {
    abilities.Add("Heal", new Healing_Ability(1f, 5f, 15f));
    abilities.Add("Elemental_Attack", new Elemental_Attack(1f, 10f));
  }

  public void Update()
  {
    ManageAbilities();
    
  }

  private void ManageAbilities()
  {
    foreach (string abilityKey in abilities.Keys)
    {
      Ability ability = abilities[abilityKey];
      ability.AbilityRuntime(gameObject);
    }
  }
}