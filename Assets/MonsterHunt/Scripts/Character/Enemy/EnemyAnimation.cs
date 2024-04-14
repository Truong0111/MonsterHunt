using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public MagicAttack magicAttack;
    
    public void OnSpawnBullet()
    {
        if(magicAttack != null)
            magicAttack.SpawnBullet();
    }
}
