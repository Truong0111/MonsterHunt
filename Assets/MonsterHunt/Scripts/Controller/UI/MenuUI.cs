using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    public List<GameObject> showUIs;

    private void Awake()
    {
        startButton.onClick.AddListener(LoadLevel);
        quitButton.onClick.AddListener(Quit);
    }

    private void OnEnable()
    {
        foreach (var ui in showUIs)
        {
            ui.SetActive(false);
        }
    }

    public void LoadLevel()
    {
        LoadSceneManager.Instance.Load(1);
        foreach (var ui in showUIs)
        {
            ui.SetActive(true);
        }

        gameObject.SetActive(false);
    }

    public void Quit()
    {
#if UNITY_EDITOR
            
#else
        Application.Quit();
#endif
    }
}