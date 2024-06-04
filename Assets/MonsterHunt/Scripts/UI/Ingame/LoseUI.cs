using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    public IntEvent loseUIEvent;
    
    public Button reviveButton;
    public Button menuButton;

    private void Awake()
    {
        loseUIEvent.Register(ShowLoseUI);
        
        reviveButton.onClick.AddListener(Revive);
        menuButton.onClick.AddListener(Menu);
        
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        loseUIEvent.Unregister(ShowLoseUI);
    }

    private void ShowLoseUI(int level)
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void Revive()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void Menu()
    {
        UIController.Instance.ShowMenu(true);
        LoadSceneManager.Instance.Unload(Pref.LevelPref.Level + 2);
        Pref.LevelPref.Level = 0;
        gameObject.SetActive(false);
    }
}