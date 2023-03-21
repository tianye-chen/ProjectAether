using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing_Ability", menuName = "Ability/Healing_Ability")]
public class Healing_Ability : Ability
{

    private float addHealthAbsolute = 0f, addHealthPercentage = 0f;
    public override IEnumerator Use(CharacterBase character1, CharacterBase character2)
    {
        InitiateAbility(character1);
        character1.health += addHealthAbsolute + (character1.maxHealth * addHealthPercentage);
        yield return new WaitForSeconds(1f);
        EndAbility(character1);
    }
}
