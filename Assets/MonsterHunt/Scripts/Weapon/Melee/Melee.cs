using System;
using UnityEngine;

public class Melee : Weapon
{
    public override void Setup(WeaponData weaponData)
    {
        base.Setup(weaponData);
        currentWeaponData.meleeData = weaponData.meleeData.Clone();
    }

    private float _timeToNextAttack;

    private void Update()
    {
        OnAttack();
    }

    public override void StartAttack()
    {
        base.StartAttack();
        player.UpdateState(CharacterAction.Attack);
        _timeToNextAttack = 0;
    }

    public override void OnAttack()
    {
        base.OnAttack();
        if (!OnAttacking) return;
        _timeToNextAttack -= Time.deltaTime;
        if (_timeToNextAttack > 0) return;
        _timeToNextAttack = currentWeaponData.meleeData.attackDelay;
        Attack();
    }

    public override void StopAttack()
    {
        player.UpdateState(CharacterAction.Idle);
    }
    
    private void Attack()
    {
        AnimateAttack();
        if (!Physics.Raycast(Origin(), Direction(), out var hit, AttackDistance(), AttackLayer())) return;
        if (hit.collider.TryGetComponent<Hitbox>(out var hitBox))
        {
            CastAttack(hitBox);
        }
    }

    private void CastAttack(Hitbox hit)
    {
        hit.GetDamage(currentWeaponData.damage);
    }
    
    private void AnimateAttack()
    {
        
    }
}