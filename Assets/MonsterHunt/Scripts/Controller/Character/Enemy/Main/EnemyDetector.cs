using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public EnemyController enemyController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            enemyController.SetPlayerToAttack(player);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            enemyController.SetPlayerInRange(false);
        }
    }
}