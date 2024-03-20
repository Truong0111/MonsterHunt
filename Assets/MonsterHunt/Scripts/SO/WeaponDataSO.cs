using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(WeaponDataSO), menuName = "MonsterHunt/WeaponDataSO", order = -1)]
public class WeaponDataSO : ScriptableObject
{
    public List<WeaponData> weaponDatas;
    
    public WeaponData defaultWeapon;
    public WeaponData sword;
    
}

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

[Serializable]
public struct GunData
{
    public Bullet bullet;
    public GameObject bulletPrefab;
    
    public float fireRate;
    public int currentBullet;
    public int magazine;
    public int remainBullet;
    public bool isInfinityBullet;
    
    public float bulletSpeed;
    public float lifeTime;
    public FireType fireType;
    [FormerlySerializedAs("bulletType")] public BulletSpawnType bulletSpawnType;

    public GunData Clone()
    {
        return (GunData)MemberwiseClone();
    }
}

[Serializable]
public struct MeleeData
{
    public float attackDelay;
    public float attackSpeed;
    
    public MeleeData Clone()
    {
        return (MeleeData)MemberwiseClone();
    }
}

public enum FireType
{
    Semi,
    Auto
}

public enum BulletSpawnType
{
    None,
    Spawn,
}

public enum BulletType
{
    Five,
    Seven
}

