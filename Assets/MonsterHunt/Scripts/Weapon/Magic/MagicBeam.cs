using System;
using UnityEngine;

public class MagicBeam : MagicAttackObject
{
    public float defaultRange = 12f;
    private float _range;

    private void Awake()
    {
        _range = defaultRange;
    }

    public void SetRange(float range)
    {
        _range = range;
    }
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, _range, layerMask))
        {
            if (hit.collider.TryGetComponent<Player>(out var player))
            {
                player.GetDamage(Damage);
            }
        }
    }
}