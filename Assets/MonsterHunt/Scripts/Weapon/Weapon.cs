using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Player player;
    public WeaponDataSO weaponDataSo;
    public WeaponData currentWeaponData;
    
    
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

    #region Action

    public virtual void StartAttack()
    {
        OnAttacking = true;
        // player.Animator.SetTrigger(AnimatorConstant.Attack1);
    }
    
    public virtual void OnAttack()
    {
    }
    
    public virtual void StopAttack()
    {
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