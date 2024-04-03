using System;
using DG.Tweening;
using UnityEngine;

public class SuicideAttack : EnemyAttack
{
    public ParticleSystem suicideEffect;

    private float Radius() => EnemyData().enemyAttackValue.attackDistance;
    private Vector3 Origin() => transform.position;
    private Vector3 Direction() => transform.forward;
    private RaycastHit[] _hits;

    private bool _isSuicided;

    private void OnDisable()
    {
        SimplePool.Spawn(suicideEffect, transform.position, Quaternion.identity);
    }

    public override void Attack(float delay)
    {
        if (_isSuicided) return;
        _hits = new RaycastHit[10];
        _isSuicided = true;

        DOVirtual.DelayedCall(delay, () =>
        {
            var hitCount = Physics.SphereCastNonAlloc(Origin(),
                Radius(), Direction(), _hits, Radius(), attackMask);

            for (var i = 0; i < hitCount; i++)
            {
                Debug.Log(_hits[i].collider.name);
                if (_hits[i].collider.TryGetComponent<Player>(out var player))
                {
                    player.GetDamage(EnemyData().enemyAttackValue.damage);
                }
            }
        }).OnComplete(() => gameObject.SetActive(false));
    }
}