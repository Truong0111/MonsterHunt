using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public CharacterContainer CharacterContainer { get; set; }
    
    public Transform playerStartTransform;
    public Player Player { get; set; }

    public List<Area> areas = new();

    public Teleport teleport;
    
    #region Get - Set - Remove

    public void GetPlayer(Player player)
    {
        Player = player;
    }

    #endregion

    public override void Awake()
    {
        base.Awake();
        for (var i = 0; i < areas.Count; i++)
        {
            var area = areas[i];
            area.id = i;
        }

        teleport.gameObject.SetActive(false);
        CharacterContainer = GameManager.Instance.characterContainer;
    }

    private void Start()
    {
        SpawnPlayer();
    }

    public void OpenTeleport(Area areaClear)
    {
        if (areaClear.id == areas.Count - 1)
        {
            teleport.gameObject.SetActive(true);
        }
    }

    public void SpawnPlayer()
    {
        var player = CharacterContainer.GetPlayer(PlayerRole.Default);
        player.GetComponent<PlayerController>().MovePlayerController(playerStartTransform);
        Player = player;

        if (player.gameObject.scene != SceneManager.GetSceneAt(1))
        {
            SceneManager.MoveGameObjectToScene(player.gameObject, SceneManager.GetSceneAt(1));
        }
    }
}