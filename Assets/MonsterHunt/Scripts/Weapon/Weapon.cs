using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;
    public WeaponDataSO weaponDataSo;
    public WeaponData currentWeaponData;

    public ParticleSystem fxHit;

    public bool CanReload => 
        currentWeaponData.gunData.bullet != null 
        && currentWeaponData.gunData.remainBullet != 0 
        && currentWeaponData.gunData.currentBullet != currentWeaponData.gunData.magazine;

    public bool IsReloading { get; set; }
    
    public Vector3 Origin() => player.cameraTransform.position;
    public Vector3 Direction() => player.cameraTransform.forward;
    public float AttackDistance() => currentWeaponData.attackDistance;
    public LayerMask AttackLayer() => currentWeaponData.attackLayer;

    protected bool OnAttacking;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public virtual void Setup(WeaponData weaponData)
    {
        currentWeaponData = weaponData.Clone();
    }

    public virtual void SpawnFx(Vector3 pos)
    {
        if (fxHit == null) return;
        SimplePool.Spawn(fxHit, pos, Quaternion.identity);
    }

    #region Action

    public virtual void StartAttack()
    {
        OnAttacking = true;
        player.Animator.SetBool(AnimatorConstant.IsAttackingHash, true);
        // player.Animator.SetTrigger(AnimatorConstant.Attack1);
    }

    public virtual void OnAttack()
    {
    }

    public virtual void StopAttack()
    {
        player.Animator.SetBool(AnimatorConstant.IsAttackingHash, false);
        OnAttacking = false;
    }

    public virtual void Reload()
    {
    }

    public virtual void SpawnBullet()
    {
    }

    #endregion
}