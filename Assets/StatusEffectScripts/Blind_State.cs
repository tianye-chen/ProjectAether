using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


// This state causes the camera to be zoomed in, can only be used against the player.
public class Blind_State : State
{
  private float timer;
  private float duration;
  private float amplitude;
  private CameraController camera; 

  // stateName as required by the State class
  public override string stateName {get => "Blind";}

  // The camera is zoomed in by the duration and the amplitude
  // Lower amplitude means the camera is zoomed in more
  public Blind_State(float duration, float amplitude)
  {
    timer = 0;
    this.duration = duration;
    this.amplitude = amplitude;
  }

  public override void Enter()
  {
    camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    camera.SetCameraSize(amplitude * camera.baseZoom);
  }

  public override void Update()
  {
    if (timer < duration)
    {
      timer += Time.deltaTime;
    } else 
    {
      timer = 0;
      camera.SetCameraSize(camera.baseZoom);
      Exit();
    }
  }
}
