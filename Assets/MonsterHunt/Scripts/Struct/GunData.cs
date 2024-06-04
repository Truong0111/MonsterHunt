using System;
using UnityEngine;
using UnityEngine.Serialization;

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