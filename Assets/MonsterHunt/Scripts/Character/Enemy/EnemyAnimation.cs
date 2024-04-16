using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;
    
    public MagicAttack magicAttack;
    public MeleeWeapon meleeWeapon;
    
    public void OnSpawnBullet()
    {
        if (magicAttack != null)
            magicAttack.SpawnBullet();
    }

    public void OnAttackCall()
    {
        if (meleeWeapon != null)
        {
            meleeWeapon.CanDamage = true;
        }
    }

    public void OnAttackStop()
    {
        if (meleeWeapon != null)
        {
            meleeWeapon.CanDamage = false;
        }

        if (magicAttack != null)
        {
            magicAttack.StopAttack();
        }
    }
}
