using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{
    public CharacterController characterController;
    public CharacterData currentCharacterData;

    public int id;
    public float health;
    public float speed;
    public Animator animator;
    public List<Weapon> weapons;
    public CharacterAction characterAction;

    public bool canMove;

    public virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public virtual void OnEnable()
    {
        characterAction = CharacterAction.Idle;
    }

    public virtual void Init(float speedData, float healthData)
    {
        speed = speedData;
        health = healthData;
    }

    public Animator Animator => animator;
    public CharacterController CharacterController => characterController;

    public float CurrentHealth => health;

    public bool CanMove() => canMove;

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }


    #region State

    protected virtual void Die()
    {
        UpdateState(CharacterAction.Die);
    }

    protected virtual void Attack()
    {
        UpdateState(CharacterAction.Attack);
    }

    protected virtual void Idle()
    {
        UpdateState(CharacterAction.Idle);
    }

    protected virtual void Move()
    {
        UpdateState(CharacterAction.Move);
    }

    protected virtual void Victory()
    {
        UpdateState(CharacterAction.Victory);
    }

    public void UpdateState(CharacterAction action)
    {
        characterAction = action;
    }

    #endregion
}