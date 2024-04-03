using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public PlayerDataSO playerDataSo;
    public PlayerData playerData;
    public Camera playerCamera;
    public Transform cameraTransform;

    public PlayerData currentPlayerData;
    public Weapon CurrentWeapon { get; set; }

    public bool CanAttack { get; set; } = true;

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
    }

    public override void GetDamage(float damage)
    {
        Health -= damage;
        CallUpdatePlayerInfo();
        if (Health <= 0) Die();
    }

    private void CallUpdatePlayerInfo()
    {
        PlayerInfoUI.UpdateHealthBar.Invoke(Health / currentCharacterData.maxHealth);
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