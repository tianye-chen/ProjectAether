using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Blind_State : State
{
    float timer;

    private CharacterBase character;

    public Blind_State(CharacterBase character, string name)
    {
        this.character = character;
        timer = 50f;
        Name = name;
    }

 


    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
            character.stateMachine.RemoveState(new Blind_State(character, "blinded"));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
