using System;
using TMPro;
using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    public TextMeshProUGUI bulletText;

    public static Action<WeaponData> UpdateBullet;

    public void Awake()
    {
        UpdateBullet += UpdateBulletText;
    }

    private void UpdateBulletText(WeaponData weaponData)
    {
        var currentBullet = weaponData.IsGun ? weaponData.gunData.currentBullet : 1;
        var remainBullet = weaponData.IsGun ? weaponData.gunData.remainBullet : 0;
        var currentString =
            $"{(currentBullet <= 0 ? "<color=red>" : "<color=green>")}" +
            $"{currentBullet}</color>";
        var remainString =
            $"{(remainBullet <= 0 ? remainBullet == -1 ? "<color=yellow>" : "<color=red>" : "<color=green>")}" +
            $"{remainBullet}</color>";
        bulletText.text = $"{currentString} / {remainString}";
    }
}