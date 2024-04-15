using System;
using DG.Tweening;
using UnityEngine;

public class MagicAttack : RangeAttack
{
    public MagicBullet projectileMagic;
    public MagicBeam beamMagic;
    
    public enum MagicAttackType
    {
        Projectile,
        Beam,
    }

    public MagicAttackType magicAttackType;

    private void Awake()
    {
        if (magicAttackType == MagicAttackType.Beam)
        {
            enemy.Animator.SetInteger(AnimatorConstant.AttackValueHashed, 1);
            beamMagic.Damage = enemy.enemyData.enemyAttackValue.damage;
        }
    }

    public override void Attack(float delay)
    {
        base.Attack(delay);
        CanAttack = true;
        if (magicAttackType == MagicAttackType.Projectile)
        {
            transform.DOLookAt(player.transform.position, 0.2f);
        }
    }

    public override void StopAttack()
    {
        CanAttack = false;
        beamMagic.gameObject.SetActive(false);
    }
    
    public void SpawnBullet()
    {
        if(!CanAttack) return;
        switch (magicAttackType)
        {
            case MagicAttackType.Projectile:
                ProjectileAttack();
                break;
            case MagicAttackType.Beam:
                BeamAttack();
                break;
        }
    }

    public void ProjectileAttack()
    {
        var projectile = SimplePool.Spawn(projectileMagic);
        var direction = (player.enemyRangeAttackPoint.position - skillStartTransform.position).normalized;
        projectile.transform.SetPositionAndRotation(skillStartTransform.position, skillStartTransform.rotation);
        projectile.Direction = direction;
        projectile.Damage = enemy.enemyData.enemyAttackValue.damage;
        projectile.Speed = enemy.enemyData.enemyAttackValue.speedBullet;
        projectile.CanMove = true;
    }

    public void BeamAttack()
    {
        transform.DOLookAt(player.transform.position, 0.12f);
        beamMagic.gameObject.SetActive(true);
        beamMagic.SetRange(enemy.enemyData.enemyAttackValue.attackRange);
        beamMagic.transform.LookAt(player.enemyRangeAttackPoint);
    }
}