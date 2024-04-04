using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public BoolEvent pauseEvent;
    
    private PlayerInput _playerInput;

    public GameObject menu;
    
    public Button menuButton;
    public Button resumeButton;

    private bool _isPause;
    
    public List<GameObject> showUIs;
    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.UI.Pause.performed += e => OnPress();
        
        menuButton.onClick.AddListener(UnloadLevel);
        resumeButton.onClick.AddListener(Resume);
        
        _playerInput.Enable();
        
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        // Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        // Time.timeScale = 1f;
    }

    private void OnPress()
    {
        if(_isPause) Resume();
        else Pause();
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseEvent.Raise(true);
        foreach (var ui in showUIs)
        {
            ui.SetActive(false);
        }
        gameObject.SetActive(true);
        _isPause = true;
    }
    
    private void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseEvent.Raise(false);
        foreach (var ui in showUIs)
        {
            ui.SetActive(true);
        }
        gameObject.SetActive(false);
        _isPause = false;
    }
    
    private void UnloadLevel()
    {
        pauseEvent.Raise(false);
        menu.SetActive(true);
        LoadSceneManager.Instance.Unload(1);
        gameObject.SetActive(false);
    }
}