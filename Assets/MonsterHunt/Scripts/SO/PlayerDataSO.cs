using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = nameof(PlayerDataSO), menuName = "MonsterHunt/PlayerDataSO", order = -1)]
public class PlayerDataSO : ScriptableObject
{
    public PlayerData playerData;
}

[Serializable]
public struct PlayerData
{
    public CharacterData characterData;

    public float sprintSpeed => characterData.speed + 3;
    public float crouchSpeed => characterData.speed - 3;
    
    public PlayerData Clone()
    {
        return (PlayerData)MemberwiseClone();
    }
}