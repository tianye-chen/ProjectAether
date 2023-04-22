using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// This class is used to create a button in the inspector that calls the Generate method
[CustomEditor(typeof(AbstractGenerator), true)]
public class ProceduralGenerationEditor : Editor
{
  AbstractGenerator generator;

  private void Awake()
  {
    // Target is the inspector 
    generator = (AbstractGenerator)target;
  }

  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    // Creates a button in the inspector that calls the Generate method
    if (GUILayout.Button("Generate"))
    {
      generator.Generate();
    }
  }
}
