using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public IntEvent winGameEvent;

    public Button backToMenuButton;
    
    private void Awake()
    {
        winGameEvent.Register(ShowWinUI);
        
        backToMenuButton.onClick.AddListener(BackToMenu);
        
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        winGameEvent.Unregister(ShowWinUI);
    }

    private void ShowWinUI(int level)
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void BackToMenu()
    {
        UIController.Instance.ShowMenu(true);
        LoadSceneManager.Instance.Unload(Pref.LevelPref.Level + 2);
        Pref.LevelPref.Level = 0;
        gameObject.SetActive(false);
    }
}