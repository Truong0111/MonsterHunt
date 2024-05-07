using UnityEngine;

[Singleton(nameof(GameManager), true)]
public class GameManager : Singleton<GameManager>
{
    private int CurrentLevel { get; set; }

    public override void Awake()
    {
        base.Awake();

        Pref.LevelPref.Level = 0;
        CurrentLevel = 0;
    }

    public void LoadLevel()
    {
        CurrentLevel = Pref.LevelPref.Level;
        var levelScene = CurrentLevel + 2;
        if (CurrentLevel > Pref.LevelPref.HighestLevel)
        {
            Pref.LevelPref.HighestLevel = CurrentLevel;
        }

        LoadSceneManager.Instance.Load(levelScene);
    }

    public void LoadNextLevel()
    {
        Pref.LevelPref.Level++;
        LoadLevel();
    }
}