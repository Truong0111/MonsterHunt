using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopUI : MonoBehaviour
{
    public Button itemButton;
    public Image itemImage;

    private WeaponData _weaponData;
    
    private void Awake()
    {
        itemButton.onClick.AddListener(UpdateShop);
    }

    public void Setup(WeaponData weaponData)
    {
        
    }
    
    private void UpdateShop()
    {
        
    }
}