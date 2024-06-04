using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    private Player _player;
    private PlayerController _playerController;

    public WeaponDataSO weaponDataSo;

    public WeaponData currentWeaponData;
    public Weapon currentWeapon;
    public Weapon[] weaponsEquipped;

    private void Awake()
    {
        GameManager.Instance.PlayerInput.Weapon.Fire1Pressed.performed += AttackPressed;
        GameManager.Instance.PlayerInput.Weapon.Fire1Released.performed += AttackReleased;
        GameManager.Instance.PlayerInput.Weapon.Reload.performed += Reload;

        GameManager.Instance.PlayerInput.Weapon.Swap.performed += Swap;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        weaponsEquipped = new Weapon[3];

        _playerController = GetComponentInParent<PlayerController>();
        _player = GetComponentInParent<Player>();
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerInput.Weapon.Fire1Pressed.performed -= AttackPressed;
        GameManager.Instance.PlayerInput.Weapon.Fire1Released.performed -= AttackReleased;
        GameManager.Instance.PlayerInput.Weapon.Reload.performed -= Reload;

        GameManager.Instance.PlayerInput.Weapon.Swap.performed -= Swap;
    }

    private void Start()
    {
        SpawnDefaultWeapon(weaponDataSo.defaultWeapon);
        SpawnDefaultWeapon(weaponDataSo.sword);
        SetupCurrentWeapon(0);
    }

    private void SpawnDefaultWeapon(WeaponData weaponData)
    {
        var weapon = SimplePool.Spawn(weaponData.weapon);
        weapon.Setup(weaponData);
        var weaponTransform = weapon.transform;
        weaponTransform.SetParent(transform);
        weaponTransform.localPosition = Vector3.zero;
        weaponTransform.localRotation = Quaternion.identity;
        weaponTransform.localScale = weapon.currentWeaponData.scaleOnHand * Vector3.one;
        AddToEquippedWeapon(weapon);
        weapon.gameObject.SetActive(false);
    }

    private void SetupCurrentWeapon(int index)
    {
        currentWeapon = weaponsEquipped[index];
        currentWeapon.gameObject.SetActive(true);
        _player.CurrentWeapon = currentWeapon;
        var data = weaponsEquipped[index].currentWeaponData;
        WeaponUI.UpdateBullet.Invoke(data);
    }

    private void AddToEquippedWeapon(Weapon weapon)
    {
        for (var index = 0; index < weaponsEquipped.Length; index++)
        {
            if (weaponsEquipped[index] != null) continue;
            weaponsEquipped[index] = weapon;
            return;
        }
    }

    private void AttackPressed(InputAction.CallbackContext context)
    {
        if (currentWeapon)
        {
            currentWeapon.StartAttack();
        }
    }

    private void AttackReleased(InputAction.CallbackContext context)
    {
        if (currentWeapon)
        {
            currentWeapon.StopAttack();
        }
    }

    public virtual void Reload(InputAction.CallbackContext context)
    {
        if (!_player.CanReload) return;
        if (!_player.CurrentWeapon.CanReload) return;
        if (_player.CurrentWeapon.IsReloading) return;
        _player.CurrentWeapon.IsReloading = true;
        _playerController.Animator.SetTrigger(AnimatorConstant.ReloadHash);
        _player.CanAttack = false;
    }

    public virtual void Swap(InputAction.CallbackContext context)
    {
        var index = (int)context.ReadValue<float>() - 1;
        if (weaponsEquipped[index] == null) return;
        for (var i = 0; i < weaponsEquipped.Length; i++)
        {
            if (weaponsEquipped[i] == null) continue;
            weaponsEquipped[i].gameObject.SetActive(i == index);
        }

        SetupCurrentWeapon(index);
    }
}