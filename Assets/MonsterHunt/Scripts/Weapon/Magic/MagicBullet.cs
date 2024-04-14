using System;
using UnityEngine;

public class MagicBullet : MagicAttackObject
{
    public float despawnTime = 10f;
    private float _despawnTime;

    private bool _isDamage;

    private void OnEnable()
    {
        _isDamage = false;
        _despawnTime = despawnTime;
    }

    private void Update()
    {
        if (!CanMove) return;
        transform.position += Direction * (Speed * Time.deltaTime);

        if (Physics.SphereCast(transform.position, radius, Direction, out var hit, radius, layerMask))
        {
            if (hit.collider.TryGetComponent<Player>(out var player))
            {
                if (_isDamage) return;
                player.GetDamage(Damage);
                _isDamage = true;
            }
            else
            {
                SimplePool.Despawn(gameObject);
            }
        }
        else
        {
            _despawnTime -= Time.deltaTime;
            if (_despawnTime > 0) return;
            SimplePool.Despawn(gameObject);
        }
    }
}