using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Character
{
    public FloatEvent updateHealthBarEvent;
    public PlayerDataSO playerDataSo;
    public Camera playerCamera;
    public Transform cameraTransform;

    public PlayerData currentPlayerData;

    public Transform enemyRangeAttackPoint;
    public Weapon CurrentWeapon { get; set; }

    public bool CanAttack { get; set; } = true;
    public bool CanReload => CurrentWeapon.CanReload;

    public float MaxHealth { get; set; }

    public override void Awake()
    {
        base.Awake();
        LevelManager.Instance.GetPlayer(this);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        Setup(playerDataSo.playerData);
    }

    public void Setup(PlayerData data)
    {
        currentCharacterData = data.characterData.Clone();
        currentPlayerData = data.Clone();
        Init(currentCharacterData.speed, currentCharacterData.maxHealth);
        MaxHealth = currentCharacterData.maxHealth;
        CallUpdatePlayerInfo();
    }

    public override void GetDamage(float damage)
    {
        Health -= damage;
        CallUpdatePlayerInfo();
        if (Health <= 0) Die();
    }

    private void CallUpdatePlayerInfo()
    {
        updateHealthBarEvent.Raise(Health / MaxHealth);
    }

    public void Sprint()
    {
        Speed = currentPlayerData.sprintSpeed;
        UpdateState(CharacterAction.Sprint);
    }

    public void Crouch()
    {
        Speed = currentPlayerData.crouchSpeed;
        UpdateState(CharacterAction.Crouch);
    }
}