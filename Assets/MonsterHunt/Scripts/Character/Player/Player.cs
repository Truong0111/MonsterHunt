using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public PlayerDataSO playerDataSo;

    public Camera playerCamera;
    public Transform cameraTransform;
    
    public PlayerData currentPlayerData;
    public Weapon CurrentWeapon { get; set; }

    public override void OnEnable()
    {
        base.OnEnable();
        var data = playerDataSo.playerDatas.Find(x => x.characterData.id == id);
        Setup(data);
    }

    public void Setup(PlayerData playerData)
    {
        currentCharacterData = playerData.characterData.Clone();
        currentPlayerData = playerData.Clone();
        Init(currentCharacterData.speed, currentCharacterData.maxHealth);
    }

    public void Sprint()
    {
        speed = currentPlayerData.sprintSpeed;
        UpdateState(CharacterAction.Sprint);
    }

    public void Crouch()
    {
        speed = currentPlayerData.crouchSpeed;
        UpdateState(CharacterAction.Crouch);
    }
}