using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            player.CharacterController.enabled = false;
            player.transform.position = respawnPoint.position;
            player.CharacterController.enabled = true;

        }

        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.Die();
        }
    }
}
