using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;

    public Enemy enemy;
    
    public MagicAttack[] magicAttacks;
    public MeleeWeapon[] meleeWeapons;
    
    public void OnSpawnBullet()
    {
        foreach (var magicAttack in magicAttacks)
        {
            if (magicAttack != null)
                magicAttack.SpawnBullet();
        }
    }

    public void OnAttackCall()
    {
        foreach (var meleeWeapon in meleeWeapons)
        {
            if (meleeWeapon != null)
            {
                meleeWeapon.CanDamage = true;
            }
        }
    }

    public void OnAttackStop()
    {
        foreach (var magicAttack in magicAttacks)
        {
            if (magicAttack != null)
            {
                magicAttack.StopAttack();
            }
        }
        
        foreach (var meleeWeapon in meleeWeapons)
        {
            if (meleeWeapon != null)
            {
                meleeWeapon.CanDamage = false;
            }
        }
    }

    public void OnEndDie()
    {
        enemy.gameObject.SetActive(false);
    }
}
