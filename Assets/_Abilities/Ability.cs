using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public abstract class Ability{
  [SerializeField]
  protected float cooldown;
  protected static float cooldownTimer;
  [SerializeField]
  protected float amplitude;
  

  public float Cooldown { get => cooldown; set => cooldown = value; }
  public float Amplitude { get => amplitude; set => amplitude = value; }

  public abstract void Activate(GameObject parent);
  public abstract void AbilityRuntime(GameObject parent);

  public virtual void ManageCooldown() {
    if (cooldownTimer > 0) {
      cooldownTimer -= Time.deltaTime;
    }
  }

  public virtual void StartCooldown() {
    cooldownTimer = cooldown;
  }

  public virtual bool IsReady() {
    return cooldownTimer <= 0;
  }
}