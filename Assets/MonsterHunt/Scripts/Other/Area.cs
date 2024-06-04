using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Area : MonoBehaviour
{
    public int id;

    public Enemy[] enemies;

    public Barrier[] barriers;

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
        if (CheckClearArea())
        {
            OpenArea();
        }
    }

    private bool CheckClearArea()
    {
        return enemies.All(enemy => enemy.characterAction == CharacterAction.Die);
    }

    private void OpenArea()
    {
        foreach (var barrier in barriers)
        {
            barrier.OpenBarrier(true);
        }
        LevelManager.Instance.OpenTeleport(this);
    }
}