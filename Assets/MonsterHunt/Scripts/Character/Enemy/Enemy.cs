using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public PrefabSO prefabSo;
    public EnemyDataSO enemyDataSo;
    public EnemyData currentEnemyData;

    public override void Awake()
    {
        base.Awake();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        var data = enemyDataSo.enemyDatas.Find(x => x.characterData.id == id);
        Setup(data);
    }

    private void Setup(EnemyData enemyData)
    {
        currentCharacterData = enemyData.characterData.Clone();
        currentEnemyData = enemyData.Clone();
        Init(currentCharacterData.speed, currentCharacterData.maxHealth);
    }

    protected override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
        DropObject();
    }

    public void DropObject()
    {
        if (currentEnemyData.enemyDrop == EnemyDrop.Gold)
        {
            
        }

        if (currentEnemyData.enemyDrop == EnemyDrop.Bullet)
        {
        }

        if (currentEnemyData.enemyDrop == EnemyDrop.HealthItem)
        {
        }

        if (currentEnemyData.enemyDrop == EnemyDrop.Weapon)
        {
        }
    }
}