using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "General_Boost", menuName = "Ability/General_Boost")]
public class GeneralBoost : Ability
{

    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override IEnumerator Use(CharacterBase character1, CharacterBase character2)
    {
        InitiateAbility(character1);
        yield return new WaitForSeconds(1f);
        character1.speed = (int)(character1.speed * 1.3f);
        character1.atk = (int)(character1.atk * 1.3f);
        character1.def = (int)(character1.def * 1.3f);
        yield return new WaitForSeconds(1f);
        EndAbility(character1);
        yield return new WaitForSeconds(10f);
        character1.speed = (int)(character1.speed * 1f);
        character1.atk = (int)(character1.atk * 1f);
        character1.def = (int)(character1.def * 1f);
        //add more stat boosts
    }
}