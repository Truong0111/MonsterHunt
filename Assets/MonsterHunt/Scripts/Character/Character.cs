using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{
    public CharacterController characterController;
    public CharacterData currentCharacterData;

    [ShowInInspector] public float Health { get; set; }
    public float Speed { get; set; }
    public Vector3 Position => transform.position;
    
    private Animator _animator;
    public List<Weapon> weapons;
    public CharacterAction characterAction;

    public event Action CharacterDie; 
    
    public bool canMove;

    public virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();

        
    }

    public virtual void OnEnable()
    {
        characterAction = CharacterAction.Idle;
        CharacterDie += OnCharacterDie;
    }

    private void OnDisable()
    {
        CharacterDie -= OnCharacterDie;
    }

    public virtual void Init(float speedData, float healthData)
    {
        Speed = speedData;
        Health = healthData;
    }

    public Animator Animator => _animator;
    public CharacterController CharacterController => characterController;

    public float CurrentHealth => Health;

    public bool CanMove() => canMove;

    public virtual void GetDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0) Die();
    }

    public virtual void OnCharacterDie()
    {
        
    }
    
    #region State

    public virtual void Die()
    {
        if(characterAction == CharacterAction.Die) return;
        UpdateState(CharacterAction.Die);
    }

    public virtual void Attack()
    {
        UpdateState(CharacterAction.Attack);
    }

    public virtual void Idle()
    {
        UpdateState(CharacterAction.Idle);
    }

    public virtual void Move()
    {
        UpdateState(CharacterAction.Move);
    }

    public virtual void Victory()
    {
        UpdateState(CharacterAction.Victory);
    }

    public void UpdateState(CharacterAction action)
    {
        characterAction = action;
    }

    #endregion
}