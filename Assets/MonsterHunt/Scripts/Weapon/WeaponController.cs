using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private PlayerController _playerController;

    private PlayerInput _playerInput;

    public WeaponDataSO weaponDataSo;

    public WeaponData currentWeaponData;
    public Weapon currentWeapon;

    private bool _isInitialized;

    public bool isSword;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Weapon.Fire1Pressed.performed += e => AttackPressed();
        _playerInput.Weapon.Fire1Released.performed += e => AttackReleased();
        _playerInput.Weapon.Reload.performed += e => Reload();

        _playerInput.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        SetupCurrentWeapon(isSword ? weaponDataSo.sword : weaponDataSo.defaultWeapon);
    }

    private void SetupCurrentWeapon(WeaponData weaponData)
    {
        currentWeaponData = weaponData;
        currentWeapon = SimplePool.Spawn(currentWeaponData.weapon);
        currentWeapon.Setup(currentWeaponData);
        var weaponTransform = currentWeapon.transform;
        weaponTransform.SetParent(transform);
        weaponTransform.localPosition = Vector3.zero;
        weaponTransform.localRotation = Quaternion.identity;
        weaponTransform.localScale = currentWeaponData.scaleOnHand * Vector3.one;

        WeaponUI.UpdateBullet.Invoke(currentWeaponData);
    }

    private void AttackPressed()
    {
        if (currentWeapon)
        {
            currentWeapon.StartAttack();
        }
    }

    private void AttackReleased()
    {
        if (currentWeapon)
        {
            currentWeapon.StopAttack();
        }
    }

    public virtual void Reload()
    {
        currentWeapon.Reload();
    }
}