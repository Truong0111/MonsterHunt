using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Area : MonoBehaviour
{
    public int id;

    public Enemy[] enemies;

    public Barrier barrier;

    private void Start()
    {
        foreach (var enemy in enemies)
        {
            enemy.CharacterDie += GetEnemyDie;
        }
    }

    private void OnDestroy()
    {
        foreach (var enemy in enemies)
        {
            enemy.CharacterDie -= GetEnemyDie;
        }
    }

    private void GetEnemyDie()
    {
        CheckClearArea();
    }
    
    private void CheckClearArea()
    {
        var isClear = enemies.All(enemy => enemy.characterAction == CharacterAction.Die);
        barrier.OpenBarrier(isClear);
        LevelManager.Instance.OpenTeleport(this);
    }
}