using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//[CreateAssetMenu(fileName = "ElementalAttack", menuName = "Ability/ElementalAttack")]
public class Elemental_Attack : Ability
{
  private static int selectedElement;
  private float[] cooldowns = { 0f, 30f, 20f, 15f, 20f };
  private static float[] cooldownTimers = { 0f, 0f, 0f, 0f, 0f };

  public Elemental_Attack(float amplitude, float manaCost)
  {
    selectedElement = 0;
    this.amplitude = amplitude;
    this.manaCost = manaCost;
  }
   
  public override void AbilityRuntime(GameObject parent)
  {
    // scroll wheel up 
    if (Input.GetAxis("Mouse ScrollWheel") > 0f)
    {
      selectedElement--;
      if (selectedElement < 0)
      {
        selectedElement = 4;
      }
    }

    // scroll wheel down
    if (Input.GetAxis("Mouse ScrollWheel") < 0f)
    {
      selectedElement++;
      if (selectedElement > 4)
      {
        selectedElement = 0;
      }
    }

    if (Input.GetMouseButtonDown(1))
    {
      Activate(parent);
    }

    ManageCooldown();
  }

  public override void Activate(GameObject parent)
  {
    switch (selectedElement)
    {
      case 0:
        if (IsReady(selectedElement) && parent.GetComponent<CharacterBase>().checkMana(manaCost/10))
        {
          LightningAttack(parent);
          StartCooldown(selectedElement);
        }
        else
        {
          Debug.Log("Lightning Attack is on cooldown");
        }
        break;
      case 1:
        if (IsReady(selectedElement) && parent.GetComponent<CharacterBase>().checkMana(manaCost * 4))
        {
          FireAttack(parent);
          StartCooldown(selectedElement);
          //ireCooldown.text = cooldownTimers[1].ToString();
        }
        else
        {
          Debug.Log("Fire Attack is on cooldown");
        }
        break;
      case 2:
        if (IsReady(selectedElement))
        {
          WaterAttack(parent);
          StartCooldown(selectedElement);
        }
        else
        {
          Debug.Log("Water Attack is on cooldown");
        }
        break;
      case 3:
        if (IsReady(selectedElement) && parent.GetComponent<CharacterBase>().checkMana(manaCost))
        {
          AirAttack(parent);
          StartCooldown(selectedElement);
        }
        else
        {
          Debug.Log("Air Attack is on cooldown");
        }
        break;
      case 4:
        if (IsReady(selectedElement) && parent.GetComponent<CharacterBase>().checkMana(manaCost * 3))
        {
          EarthAttack(parent);
          StartCooldown(selectedElement);
        }
        else
        {
          Debug.Log("Earth Attack is on cooldown");
        }
        break;
      default:
        Debug.Log("No element selected");
        break;
    }
  }

  public override void ManageCooldown()
  {
    for(int i = 0; i < cooldownTimers.Length; i++)
    {
      if (cooldownTimers[i] > 0)
      {
        cooldownTimers[i] -= Time.deltaTime;
      }
    }
  }

  public void StartCooldown(int elementIndex)
  {
    cooldownTimers[elementIndex] = cooldowns[elementIndex];
  }

  public bool IsReady(int elementIndex)
  {
    return cooldownTimers[elementIndex] <= 0;
  }

  public void LightningAttack(GameObject parent)
  {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector3 direction = mousePos - parent.transform.position;
    float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
    Object.Instantiate(Resources.Load("Lightning"), parent.transform.position, Quaternion.Euler(0, 0, angle));
    parent.GetComponent<CharacterBase>().depleteMana(manaCost / 10);
  }

  public void FireAttack(GameObject parent)
  {
    GameObject fireAttack =  (GameObject)Object.Instantiate(Resources.Load("Fire"), parent.transform.position, Quaternion.identity);
    fireAttack.GetComponent<FireAttack>().SetDamage(0.7f * Amplitude);
    parent.GetComponent<CharacterBase>().depleteMana(manaCost * 4);
  }

  public void WaterAttack(GameObject parent)
  {
    GameObject waterAttack = (GameObject)Object.Instantiate(Resources.Load("Water"), parent.transform.position, Quaternion.identity);
    waterAttack.GetComponent<WaterAttack>().SetParent(parent);
    waterAttack.GetComponent<WaterAttack>().SetHealStrength(20 * Amplitude);
    parent.GetComponent<CharacterBase>().depleteMana(0);
  }

  public void AirAttack(GameObject parent)
  {
    GameObject airAttack = (GameObject)Object.Instantiate(Resources.Load("Wind"), parent.transform.position, Quaternion.identity);
    airAttack.GetComponent<AirAttack>().SetParent(parent);
    airAttack.GetComponent<AirAttack>().SetDamage(5 * Amplitude);
    parent.GetComponent<CharacterBase>().depleteMana(manaCost);
  }

  public void EarthAttack(GameObject parent)
  {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    if (Vector3.Distance(parent.transform.position, mousePos) > 5)
    {
      mousePos = parent.transform.position + (mousePos - parent.transform.position).normalized * 5;
    }

    Object.Instantiate(Resources.Load("Earth"), mousePos, Quaternion.Euler(0, 0, 0));
    parent.GetComponent<CharacterBase>().depleteMana(manaCost * 3);
  }

  public static int GetSelectedElement()
  {
    return selectedElement;
  }

  public static float GetRemainingCooldowns(string element)
  {
    switch (element.ToLower())
    {
      case "lightning":
        return cooldownTimers[0];
      case "fire":
        return cooldownTimers[1];
      case "water":
        return cooldownTimers[2];
      case "air":
        return cooldownTimers[3];
      case "earth":
        return cooldownTimers[4];
      default:
        return 0f;
    }
  }
}