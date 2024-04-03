using System;
using UnityEngine;

public class Gun : Weapon
{
    public ParticleSystem effectShoot;
    public Transform bulletSpawn;

    public override void Setup(WeaponData weaponData)
    {
        base.Setup(weaponData);
        currentWeaponData.gunData = weaponData.gunData.Clone();
    }

    private float _timeToNextFire;

    private void Update()
    {
        OnAttack();
    }

    public override void StartAttack()
    {
        base.StartAttack();
        _timeToNextFire = 0;
    }

    public override void OnAttack()
    {
        if(!player.CanAttack) return;
        if (!OnAttacking) return;
        if (currentWeaponData.gunData.currentBullet <= 0) return;
        _timeToNextFire -= Time.deltaTime;
        if (_timeToNextFire > 0) return;
        currentWeaponData.gunData.currentBullet--;
        _timeToNextFire = currentWeaponData.gunData.fireRate;
        Attack();

        if (currentWeaponData.gunData.fireType == FireType.Semi) StopAttack();
    }

    public override void Reload()
    {
        base.Reload();
        player.CanAttack = false;
        if (currentWeaponData.gunData.isInfinityBullet)
        {
            if (currentWeaponData.gunData.currentBullet == currentWeaponData.gunData.magazine) return;
            currentWeaponData.gunData.currentBullet = currentWeaponData.gunData.magazine;
        }
        else
        {
            if (currentWeaponData.gunData.remainBullet == 0 ||
                currentWeaponData.gunData.currentBullet == currentWeaponData.gunData.magazine) return;
            var bulletToReload = currentWeaponData.gunData.magazine - currentWeaponData.gunData.currentBullet;
            var bulletReloaded = Mathf.Min(bulletToReload, currentWeaponData.gunData.remainBullet);

            currentWeaponData.gunData.currentBullet += bulletReloaded;
            currentWeaponData.gunData.remainBullet -= bulletReloaded;
        }
        CallUpdateBulletCountText();
    }

    private void CallUpdateBulletCountText()
    {
        WeaponUI.UpdateBullet.Invoke(currentWeaponData);
    }

    private void Attack()
    {
        AnimateAttack();
        CallUpdateBulletCountText();
        if (!Physics.Raycast(Origin(), Direction(), out var hit, AttackDistance(), AttackLayer())) return;
        if (hit.collider.TryGetComponent<Hitbox>(out var hitBox))
        {
            SpawnFx(hit.point);
            hitBox.GetDamage(currentWeaponData.damage);
        }
    }

    private void AnimateAttack()
    {
        effectShoot.Play();
    }
}