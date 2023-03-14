using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularState : CasterStateController
{
    public float castTimer = 0;
    public float projectileTimer = 0;
    private GameObject Inst;

    public override void EnterState(CasterEnemyController caster)
    {
        Debug.Log("Entering Regular State");
    }

    public override void Update(CasterEnemyController caster)
    {
        if (Vector2.Distance(caster.transform.position, caster.Player.transform.position) < caster.aggroRange)
        {
            // if the player is within 1 less than the attack range, move away from the player and fire projectiles
            if (Vector2.Distance(caster.transform.position, caster.Player.transform.position) < caster.attackRange - 1)
            {
                caster.transform.position = Vector2.MoveTowards(caster.transform.position, caster.Player.transform.position, -caster.speed * Time.deltaTime);
                
                // fires projectiles in a cone in front of the caster
                projectileTimer += Time.deltaTime;
                if (projectileTimer > caster.attackSpeed)
                {
                    projectileTimer = 0;
                    
                    for (int i = 0; i < caster.numProjectiles; i++)
                    {
                        Inst = MonoBehaviour.Instantiate(caster.projectileObject, caster.transform.position, Quaternion.identity);
                        if(caster.facingEast)
                            Inst.transform.Rotate(0, 0, (-90 + 10 * caster.numProjectiles / 2) - i * 10);
                        else
                            Inst.transform.Rotate(0, 0, (90 - 10 * caster.numProjectiles / 2) + i * 10);
                    }

                    // check if the projectile is no longer within 10 units of caster
                    if (Vector2.Distance(Inst.transform.position, caster.transform.position) > 10)
                    {
                        Inst.GetComponent<BasicProjectile>().Delete();
                    }
                }

            // if the player is within the cast range enter the casting state
            } else if (Vector2.Distance(caster.transform.position, caster.Player.transform.position) < caster.attackRange)
            {
                castTimer += Time.deltaTime;
                if (castTimer > caster.castCooldown)
                {
                    castTimer = 0;
                    caster.SwitchState(caster.castingState);
                }
            }

            // if the player is outside the attack range, move towards the player
            else
            {
                caster.transform.position = Vector2.MoveTowards(caster.transform.position, caster.Player.transform.position, caster.speed * Time.deltaTime);
            }
        }
    }
}
