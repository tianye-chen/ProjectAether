using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoostAbility", menuName = "Ability/BoostAbility")]
public class BoostAbility : Ability
{
    [SerializeField]
    float accuracyBoostAbs, speedBoostAbs, atkBoostAbs, defBoostAbs;
    [SerializeField]
    float accuracyBoostRel, speedBoostRel, atkBoostRel, defBoostRel;
    public override IEnumerator Use(CharacterBase character1, CharacterBase character2)
    {
        InitiateAbility(character1);
        yield return new WaitForSeconds(1f);
        character1.speed += character1.speed * accuracyBoostRel + accuracyBoostAbs;
        character1.atk += character1.atk * atkBoostRel + atkBoostAbs;
        character1.def += character1.def * defBoostRel + defBoostAbs;
        yield return new WaitForSeconds(1f);
        EndAbility(character1);
        yield return new WaitForSeconds(10f);
        character1.speed = (character1.maxSpeed);
        character1.atk = (character1.maxAtk);
        character1.def = (character1.maxDef);
        character1.accuracy = (character1.maxAccuracy);
        //add more stat boosts
    }
}

