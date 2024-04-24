using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : Character
{
    public EnemyDataSO enemyDataSo;
    public EnemyData enemyData;
    public EnemyController enemyController;

    public EnemyDetector enemyDetector;
    public SphereCollider rangeSphere;

    public EnemyWeapon[] enemyWeapons;
    
    public override void Awake()
    {
        base.Awake();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        Setup(enemyDataSo.enemyData);
    }

    private void Setup(EnemyData data)
    {
        currentCharacterData = data.characterData.Clone();
        enemyData = data.Clone();
        Init(currentCharacterData.speed, currentCharacterData.maxHealth);
        enemyController.AttackRange = this.enemyData.enemyAttackValue.attackRange;
        enemyController.NoAttackMove = this.enemyData.enemyAttackValue.attackRange * 2f;
        enemyController.SetSpeed(this.enemyData.characterData.speed);

        enemyDetector.SetRange(enemyData.enemyAttackValue.attackDetect);
        rangeSphere.radius = enemyData.enemyAttackValue.attackRange;
    }

    public override void Die()
    {
        base.Die();
        enemyController.SetDead();
        DropObject();
    }

    public void DropObject()
    {
        if (enemyData.enemyDrop == EnemyDrop.Gold)
        {
            
        }

        if (enemyData.enemyDrop == EnemyDrop.Bullet)
        {
        }

        if (enemyData.enemyDrop == EnemyDrop.HealthItem)
        {
        }

        if (enemyData.enemyDrop == EnemyDrop.Weapon)
        {
        }
    }
}