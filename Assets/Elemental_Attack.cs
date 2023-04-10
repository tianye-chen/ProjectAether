using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementalAttack", menuName = "Ability/ElementalAttack")]
public class Elemental_Attack : Ability
{
   public enum element { Water = 1, Fire = 2, Wind = 3, Earth = 4, Electricity = 5};
    public element elementalAtk;

    public enum atkPower { Weak, Moderate, Strong};
    public atkPower AtkPower;

    public override IEnumerator Use(CharacterBase character1, CharacterBase character2 = null)
    {
        InitiateAbility(character1);
        float atk = character1.atk * 4;
        switch (AtkPower)
        {
            case atkPower.Weak:
                atk = character1.atk * 4;
                break;
            case atkPower.Moderate:
                atk = character1.atk * 5;
                break;
            case atkPower.Strong:
                atk = character1.atk * 6;
                break;
        }

        yield return new WaitForSeconds(1f);
        float damage = atk - character2.def;
        if ((int)elementalAtk + 1 == (int)character2.SelfElement||
            ((int)elementalAtk == 5 && (int)elementalAtk == 1))
            damage *= 2;
        character2.TakeDamage(damage);
        EndAbility(character1);
    }
}