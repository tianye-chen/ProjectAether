using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : CasterStateController
{
    private float timer = 0;

    public override void EnterState(CasterEnemyController caster)
    {
        Debug.Log("Entering Stunned State");
    }

    public override void Update(CasterEnemyController caster)
    {
        // stun for the stun time
        timer += Time.deltaTime;
        if (timer > caster.stunTime)
        {
            caster.SwitchState(caster.regularState);
            timer = 0;
        }
    }
}
