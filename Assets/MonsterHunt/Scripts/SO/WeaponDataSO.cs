using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(WeaponDataSO), menuName = "MonsterHunt/WeaponDataSO", order = -1)]
public class WeaponDataSO : ScriptableObject
{
    public List<WeaponData> gunDatas;
    public List<WeaponData> swordDatas;

    public WeaponData defaultWeapon;
    public WeaponData sword;

    public int GetGunsCount() => gunDatas.Count;
    public List<WeaponData> GetGuns() => gunDatas;
    public int GetSwordsCount() => gunDatas.Count;
    public List<WeaponData> GetSwords() => gunDatas;

    public List<WeaponData> GetWeapons()
    {
        var weapons = new List<WeaponData>();
        weapons.AddRange(gunDatas);
        weapons.AddRange(swordDatas);
        return weapons;
    }
}