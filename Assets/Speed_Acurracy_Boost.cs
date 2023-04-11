using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Speed_Accuracy_Boost", menuName = "Ability/Speed_Accuracy_Boost")]
public class Speed_Accuracy_Boost : Ability { 


    public override IEnumerator Use(CharacterBase character1, CharacterBase character2)
{
    InitiateAbility(character1);
    yield return new WaitForSeconds(1f);
    character1.speed = (int)(character1.speed * 1.6f);
    character1.accuracy = (int)(character1.maxAccuracy * 1.6f);
    yield return new WaitForSeconds(1f);
    EndAbility(character1);
    yield return new WaitForSeconds(10f);
    character1.speed = (int)(character1.speed * 1f);
    character1.accuracy = (int)(character1.maxAccuracy);
    }
}