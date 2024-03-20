using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = nameof(PrefabSO), menuName = "MonsterHunt/PrefabSO",order = -1)]

public class PrefabSO : ScriptableObject
{
    public CoinDrop coinDrop;
    public HealthItem healthItem;
    public BulletBox bulletBox;
    public WeaponDrop weaponDrop;

    public void SpawnCoin()
    {
        var co = SimplePool.Spawn(coinDrop);
        co.count = Random.Range(0, 50);
    }

    public void SpawnHealthItem()
    {
        var ht = SimplePool.Spawn(healthItem);
        ht.healthNumber = Random.Range(10f, 15f);
    }

    public void SpawnBulletBox()
    {
        var bb = SimplePool.Spawn(bulletBox);
        bb.bulletType = Random.value < 0.5 ? BulletType.Five : BulletType.Seven;
        bb.count = 30;
    }

    public void SpawnWeaponDrop()
    {
        var wd = SimplePool.Spawn(weaponDrop);
    }
}
