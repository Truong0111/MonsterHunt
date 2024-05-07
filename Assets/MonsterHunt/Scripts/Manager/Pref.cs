using UnityEngine;

public class Pref
{
    public class LevelPref
    {
        public const string CurrentLevelKey = "Current_Level";
        public const string HighestLevelKey = "Highest_level";

        public static int Level
        {
            get => PlayerPrefs.GetInt(CurrentLevelKey, 0);
            set => PlayerPrefs.SetInt(CurrentLevelKey, value);
        }

        public static int HighestLevel
        {
            get => PlayerPrefs.GetInt(HighestLevelKey, 0);
            set => PlayerPrefs.SetInt(HighestLevelKey, value);
        }
    }
}