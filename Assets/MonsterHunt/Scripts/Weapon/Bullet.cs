using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float lifeTime;
    public float damage;

    public Rigidbody rigid;

    private Coroutine _coroutine;
    
    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void Setup(float speed, float life, float atk)
    {
        bulletSpeed = speed;
        lifeTime = life;
        damage = atk;
        rigid.isKinematic = true;
    }

    public void ApplyMove()
    {
        rigid.isKinematic = false;
        rigid.AddForce(transform.forward * bulletSpeed);
        _coroutine = StartCoroutine(Extension.CountDown(lifeTime, () => SimplePool.Despawn(gameObject)));
    }
}