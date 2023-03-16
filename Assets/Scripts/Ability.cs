using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public abstract class Ability : ScriptableObject
{
    // Start is called before the first frame update
    public enum attackAnimation { charge, release}
    public attackAnimation playWhichAnimation;
    public bool inAbility = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract IEnumerator Use(CharacterBase character1, CharacterBase character2);
    public void InitiateAbility(CharacterBase character)
    {
        character.usingAbility = true;
        character.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        character.SetAnimation(playWhichAnimation.ToString());
    }
    public void EndAbility(CharacterBase character)
    {
        character.usingAbility = false;
        character.BecomeIdle();
    }
}