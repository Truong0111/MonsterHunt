using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataSO", menuName = "MonsterHunt/CharacterDataSO", order = 0)]
public class CharacterDataSO : ScriptableObject
{
    public Dictionary<EnemyType, Enemy> Enemies;

    private Enemy GetEnemy(EnemyType type)
    {
        return Enemies[type];
    }
}