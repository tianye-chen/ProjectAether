using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileAbility", menuName = "Ability/ProjectileAbility")]
public class ProjectileAbility : Ability
{
    [SerializeField]
    int projectileSpeed = 5, projectilePower = 2;
    public GameObject projectile;

    public override IEnumerator Use(CharacterBase character1, CharacterBase character2)
    {
        InitiateAbility(character1);
        yield return new WaitForSeconds(1f);
        Vector3 direction = Vector3.zero;
        int distanceStartProject = 3;
        if (character1.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "up") direction = Vector2.up;
        else if (character1.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "right") direction = Vector2.right;
        else if (character1.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "down") direction = Vector2.down;
        else if (character1.animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "left") direction = Vector2.left;
        if(Vector3.zero == direction) { Debug.LogError("Projectile clip error.")}
        GameObject Projectile = Instantiate(projectile) as GameObject;
        Projectile.transform.position = character1.transform.position + (direction * distanceStartProject);
        projectile.GetComponent<Rigidbody2D>().velocity = direction;
        EndAbility(character1);
    }
}
