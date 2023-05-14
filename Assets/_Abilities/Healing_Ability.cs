using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Healing_Ability", menuName = "Ability/Healing_Ability")]
public class Healing_Ability : Ability
{
  public Healing_Ability(float amplitude, float cooldown)
  {    
    this.amplitude = amplitude;
    this.cooldown = cooldown;
  }

  public override void AbilityRuntime(GameObject parent)
  {
    if (Input.GetKeyDown(KeyCode.E))
    {
      Debug.Log("E pressed");
      Activate(parent);
    }

    ManageCooldown();
  }

  public override void Activate(GameObject parent)
  {
    if (!IsReady())
    {
      Debug.Log("Ability not ready, cooldown: " + cooldownTimer + " seconds");
      return;
    }

    Debug.Log("Healing ability activated");
    StartCooldown();
    float maxHealth = parent.GetComponent<CharacterBase>().maxHealth;
    float oldHealth = parent.GetComponent<CharacterBase>().health;
    float newHealth = oldHealth + 10 + (oldHealth * 0.1f * amplitude);
    parent.GetComponent<CharacterBase>().HealSelf((int)Mathf.Min(newHealth, maxHealth));
  }

  public static float GetRemainingCooldown()
  {
    return cooldownTimer;
  }
}
