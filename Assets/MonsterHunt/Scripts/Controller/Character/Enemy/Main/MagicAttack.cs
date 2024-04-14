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
        ContinuousBeam,
    }

    public MagicAttackType magicAttackType;

    public override void Attack(float delay)
    {
        base.Attack(delay);
        transform.DOLookAt(player.transform.position, 0.2f);
    }

    public void SpawnBullet()
    {
        switch (magicAttackType)
        {
            case MagicAttackType.Projectile:
                ProjectileAttack();
                break;
            case MagicAttackType.Beam:
                BeamAttack();
                break;
            case MagicAttackType.ContinuousBeam:
                ContinuousBeamAttack();
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
        var beam = SimplePool.Spawn(beamMagic);
        var direction = (player.enemyRangeAttackPoint.position - skillStartTransform.position).normalized;
        beam.transform.SetPositionAndRotation(skillStartTransform.position, skillStartTransform.rotation);
        beam.Direction = direction;
        beam.Damage = enemy.enemyData.enemyAttackValue.damage;
        beam.Speed = 5f;
        beam.CanMove = true;
    }

    public void ContinuousBeamAttack()
    {
        var beam = SimplePool.Spawn(beamMagic);
        var direction = (player.enemyRangeAttackPoint.position - skillStartTransform.position).normalized;
        beam.transform.SetPositionAndRotation(skillStartTransform.position, skillStartTransform.rotation);
        beam.Direction = direction;
        beam.Damage = enemy.enemyData.enemyAttackValue.damage;
        beam.Speed = 5f;
        beam.CanMove = true;
    }
}