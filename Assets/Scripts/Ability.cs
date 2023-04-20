using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public abstract class Ability : ScriptableObject
{
    // Start is called before the first frame update
    public enum attackAnimation { charge, release}
    public attackAnimation playWhichAnimation;
    public bool inAbility = false, inCooldown = false;
    [SerializeField]
    private WaitForSeconds cooldown = new WaitForSeconds(1f);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract IEnumerator Use(CharacterBase character1, CharacterBase character2 = null);
    public void InitiateAbility(CharacterBase character)
    {
        if (!character.usingAbility || !inCooldown) return;
        character.usingAbility = true;
        inCooldown = true;
        character.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        character.SetAnimation(playWhichAnimation.ToString());
    }
    public void EndAbility(CharacterBase character)
    {
        character.usingAbility = false;
        character.BecomeIdle();
        EndCooldown();
    }
    public IEnumerator EndCooldown()
    {
        yield return new Cooldown();
        inCooldown = false;

    }
}