using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(EnemyDataSO), menuName = "MonsterHunt/EnemyDataSO", order = -1)]
public class EnemyDataSO : ScriptableObject
{
    public EnemyData enemyData;
}

[Serializable]
public struct EnemyData
{
    public CharacterData characterData;
    public EnemyType enemyType;
    public EnemyDrop enemyDrop;
    public EnemyAttackValue enemyAttackValue;
    public Enemy enemy;
    
    public EnemyData Clone()
    {
        return (EnemyData)MemberwiseClone();
    }
}

