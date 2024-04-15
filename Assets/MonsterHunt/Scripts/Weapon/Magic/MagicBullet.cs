using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MagicBullet : MagicAttackObject
{
    public ParticleSystem muzzleFX;
    public ParticleSystem missileFX;
    public ParticleSystem explosionFX;
    
    public float despawnTime = 10f;
    private float _despawnTime;

    private bool _isDamage;

    private void OnEnable()
    {
        _isDamage = false;
        _despawnTime = despawnTime;
        muzzleFX.gameObject.SetActive(true);
        missileFX.gameObject.SetActive(true);
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
                StartCoroutine(Explode());
            }
            else
            {
                explosionFX.Play();
                StartCoroutine(Explode());
            }
        }
        else
        {
            _despawnTime -= Time.deltaTime;
            if (_despawnTime > 0) return;
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        missileFX.gameObject.SetActive(false);
        yield return new WaitUntil(() => !missileFX.isPlaying);
        explosionFX.gameObject.SetActive(true);
        yield return new WaitUntil(() => !explosionFX.isPlaying);
        muzzleFX.gameObject.SetActive(false);
        explosionFX.gameObject.SetActive(false);
        SimplePool.Despawn(gameObject);
    }
}