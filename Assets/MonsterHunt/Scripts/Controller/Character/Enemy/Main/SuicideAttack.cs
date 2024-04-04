using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        CheckExplosion();
        var fx = SimplePool.Spawn(suicideEffect, transform.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(fx.gameObject, SceneManager.GetSceneAt(1));
    }

    public override void Attack(float delay)
    {
        if (_isSuicided) return;
        _isSuicided = true;
        DOVirtual.DelayedCall(delay, () => gameObject.SetActive(false));
    }

    private void CheckExplosion()
    {
        _hits = new RaycastHit[10];
        var hitCount = Physics.SphereCastNonAlloc(Origin(),
            Radius(), Direction(), _hits, Radius(), attackMask);

        for (var i = 0; i < hitCount; i++)
        {
            if (_hits[i].collider.TryGetComponent<Player>(out var player))
            {
                player.GetDamage(EnemyData().enemyAttackValue.damage);
            }
        }
    }
}