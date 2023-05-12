using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "ElementalAttack", menuName = "Ability/ElementalAttack")]
public class Elemental_Attack : Ability
{
  private static int selectedElement;
  private float[] cooldowns = { 0f, 30f, 20f, 15f, 20f };
  private float[] cooldownTimers = { 0f, 0f, 0f, 0f, 0f };

  public Elemental_Attack(float amplitude)
  {
    selectedElement = 0;
    this.amplitude = amplitude;
  }
   
  public override void AbilityRuntime(GameObject parent)
  {
    // scroll wheel up 
    if (Input.GetAxis("Mouse ScrollWheel") > 0f)
    {
      selectedElement++;
      if (selectedElement > 4)
      {
        selectedElement = 0;
      }
    }

    // scroll wheel down
    if (Input.GetAxis("Mouse ScrollWheel") < 0f)
    {
      selectedElement--;
      if (selectedElement < 0)
      {
        selectedElement = 4;
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
        if (IsReady(selectedElement))
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
        if (IsReady(selectedElement))
        {
          FireAttack(parent);
          StartCooldown(selectedElement);
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
        if (IsReady(selectedElement))
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
        if (IsReady(selectedElement))
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
    Debug.Log("Lightning Attack");
  }

  public void FireAttack(GameObject parent)
  {
    GameObject fireAttack =  (GameObject)Object.Instantiate(Resources.Load("Fire"), parent.transform.position, Quaternion.identity);
    fireAttack.GetComponent<FireAttack>().SetDamage(0.7f * Amplitude);
  }

  public void WaterAttack(GameObject parent)
  {
    GameObject waterAttack = (GameObject)Object.Instantiate(Resources.Load("Water"), parent.transform.position, Quaternion.identity);
    waterAttack.GetComponent<WaterAttack>().SetParent(parent);
    waterAttack.GetComponent<WaterAttack>().SetHealStrength(10 * Amplitude);
  }

  public void AirAttack(GameObject parent)
  {
    GameObject airAttack = (GameObject)Object.Instantiate(Resources.Load("Wind"), parent.transform.position, Quaternion.identity);
    airAttack.GetComponent<AirAttack>().SetParent(parent);
    airAttack.GetComponent<AirAttack>().SetDamage(5 * Amplitude);
  }

  public void EarthAttack(GameObject parent)
  {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    if (Vector3.Distance(parent.transform.position, mousePos) > 5)
    {
      mousePos = parent.transform.position + (mousePos - parent.transform.position).normalized * 5;
    }

    Object.Instantiate(Resources.Load("Earth"), mousePos, Quaternion.Euler(0, 0, 0));
  }

  public static int GetSelectedElement()
  {
    return selectedElement;
  }
}