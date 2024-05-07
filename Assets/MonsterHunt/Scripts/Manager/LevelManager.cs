using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
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
    }

    public void OpenTeleport(Area areaClear)
    {
        if (areaClear.id == areas.Count - 1)
        {
            teleport.gameObject.SetActive(true);
        }
    } 
}