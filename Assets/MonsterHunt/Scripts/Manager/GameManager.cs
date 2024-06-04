using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

[Singleton(nameof(GameManager), true)]
public class GameManager : Singleton<GameManager>
{
    public VoidEvent onLostFocusEvent;
    public CharacterContainer characterContainer;
    public IntEvent winGameEvent;

    private int CurrentLevel { get; set; }

    public PlayerInput PlayerInput { get; private set; }

    public override void Awake()
    {
        base.Awake();

        Pref.LevelPref.Level = 0;
        CurrentLevel = 0;

        PlayerInput = new PlayerInput();

        PlayerInput.Enable();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            PlayerInput.Enable();
        }
        else
        {
            onLostFocusEvent.Raise();
            PlayerInput.Disable();
        }
    }

    public void LoadLevel()
    {
        CurrentLevel = Pref.LevelPref.Level;
        var levelScene = CurrentLevel + 2;
        if (CurrentLevel > Pref.LevelPref.HighestLevel)
        {
            Pref.LevelPref.HighestLevel = CurrentLevel;
        }

        if (CurrentLevel > SceneManager.sceneCountInBuildSettings - 2)
        {
            winGameEvent.Raise(CurrentLevel - 1);
            return;
        }

        LoadSceneManager.Instance.Load(levelScene);
    }

    public void LoadNextLevel()
    {
        Pref.LevelPref.Level++;
        LoadLevel();
    }
}