using System;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponTouch : MeleeWeapon
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            if(!CanDamage) return;
            player.GetDamage(enemy.enemyData.enemyAttackValue.damage);
        }
    }
}