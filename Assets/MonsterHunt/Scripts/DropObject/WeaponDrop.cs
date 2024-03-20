using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponDrop : MonoBehaviour
{
    public WeaponDataSO weaponDataSo;
    public Weapon weapon;

    private void Awake()
    {
        weapon = RandomWeapon();
    }

    private Weapon RandomWeapon()
    {
        var weaponCount = weaponDataSo.weaponDatas.Count;
        return weaponDataSo.weaponDatas[Random.Range(0, weaponCount)].weapon;
    }

    public WeaponDrop(Weapon weapon)
    {
        this.weapon = weapon;
    }
}