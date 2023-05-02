using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a ScriptableObject, which is a Unity-specific class that allows us to create assets that can be edited in the inspector.
// This create flexibility to store pre-made parameters for the random walk algorithm.
// This ScriptableObject will hold the data for the random walk algorithm
[CreateAssetMenu(fileName = "RandomWalkParams_", menuName = "Procedural Generation/Random Walk Data")]
public class RandomWalkData : ScriptableObject
{
  public int iterations = 10;
  public int walkLength = 10;
  public bool startRandomlyEachIteration = true;
}
