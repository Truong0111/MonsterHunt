using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Button startButton;

    public List<GameObject> showUIs;
    
    private void Awake()
    {
        startButton.onClick.AddListener(LoadLevel);
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
        gameObject.SetActive(false);

        foreach (var ui in showUIs)
        {
            ui.SetActive(true);
        }
    }
}
