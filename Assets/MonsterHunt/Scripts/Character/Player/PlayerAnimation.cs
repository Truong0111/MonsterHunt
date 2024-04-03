using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Player player;

    private void Awake()
    {
        if (player == null) player = GetComponentInParent<Player>();
    }

    public void OnAmmunitionFill()
    {
        player.CurrentWeapon.Reload();
    }

    public void OnAnimationEndedReload()
    {
        player.CanAttack = true;
    }

    public void OnEjectCasing()
    {
        player.Attack();
    }
}
