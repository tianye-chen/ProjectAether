using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlowAbility", menuName = "Ability/SlowAbility")]
public class SlowAbility : Ability
{
    public override IEnumerator Use(CharacterBase character1, CharacterBase character2 = null)
    {
        InitiateAbility(character1);
        yield return new WaitForSeconds(1f);
        character2.speed = character2.maxSpeed / 2;
        EndAbility(character1);
        yield return new WaitForSeconds(10f);
        character2.speed = character2.maxSpeed;
    }
}
