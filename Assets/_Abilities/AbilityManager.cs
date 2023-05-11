using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
  [SerializeField]
  private List<Ability> abilities = new List<Ability>();

  public void Start()
  {
    abilities.Add(new Healing_Ability());
  }

  public void Update()
  {
    // On E press, activate the first ability
    if (Input.GetKeyDown(KeyCode.E))
    {
      ActivateAbility(0);
    }
  }

  private void ActivateAbility(int index)
  {
    abilities[index].Activate(this.gameObject);
  }
}