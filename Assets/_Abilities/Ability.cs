using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public abstract class Ability : MonoBehaviour{
  [SerializeField]
  protected float cooldown = 0f;
  [SerializeField]
  protected float amplitude = 0f;

  public float Cooldown { get => cooldown; set => cooldown = value; }
  public float Amplitude { get => amplitude; set => amplitude = value; }

  public abstract void Activate(GameObject parent);
}