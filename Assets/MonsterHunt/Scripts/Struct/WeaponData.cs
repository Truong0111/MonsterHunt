using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public struct WeaponData
{
    public Weapon weapon;
    public GameObject prefab;
    public LayerMask attackLayer;

    public short id;
    public string name;
    public WeaponType weaponType;
    public float damage;
    public float attackDistance;

    public float scaleOnHand;
    [ShowIf(nameof(IsGun))] public GunData gunData;
    [ShowIf(nameof(IsMelee))] public MeleeData meleeData;
    public bool IsGun => weaponType == WeaponType.Gun;
    public bool IsMelee => weaponType == WeaponType.Melee;

    public WeaponData Clone()
    {
        return (WeaponData)MemberwiseClone();
    }
}