using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterContainer", menuName = "MonsterHunt/CharacterContainer", order = 0)]
public class CharacterContainer : SerializedScriptableObject
{
    [SerializeField] public Dictionary<EnemyRole, Enemy> Enemies;

    [SerializeField] public Dictionary<PlayerRole, Player> Players;
    
    public Player GetPlayer(PlayerRole role)
    {
        var player = SpawnPlayer(role);
        return player;
    }

    private Player SpawnPlayer(PlayerRole role)
    {
        var player = SimplePool.Spawn(Players[role]);
        return player;
    }
    
    public Enemy GetEnemy(EnemyRole role)
    {
        var enemy = SpawnEnemy(role);
        return enemy;
    }

    private Enemy SpawnEnemy(EnemyRole role)
    {
        var enemy = SimplePool.Spawn(Enemies[role]);
        return enemy;
    }
}