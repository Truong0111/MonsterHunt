using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public RectTransform content;
    
    public Button shopButton;
    public Button closeButton;
    
    public WeaponDataSO weaponDataSo;
    
    public ItemShopUI itemButtonPrefab;

    private List<ItemShopUI> ItemShopUIs { get; set; } = new();
    
    private void Awake()
    {
        shopButton.onClick.AddListener(() => ShowShop(true));
        closeButton.onClick.AddListener(() => ShowShop(false));

        SpawnItemButton();
    }

    private void Setup()
    {
        
    }
    
    private void ShowShop(bool isShow)
    {
        gameObject.SetActive(isShow);
        ShowItemButton(isShow);
    }

    private void SpawnItemButton()
    {
        var weaponDatas = weaponDataSo.GetWeapons();

        foreach (var weaponData in weaponDatas)
        {
            var itemButton = SimplePool.Spawn(itemButtonPrefab);
            itemButton.transform.SetParent(content);
            itemButton.Setup(weaponData);
            ItemShopUIs.Add(itemButton);
        }
    }
    
    private void ShowItemButton(bool isShow)
    {
        foreach (var itemShopUI in ItemShopUIs)
        {
            itemShopUI.gameObject.SetActive(isShow);
        }
    }
}