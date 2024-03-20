using UnityEngine;


public static class PlayerPrefsUtils
{
    /// <summary>
    /// Deserializes a class from JSON data stored on PlayerPrefs.
    /// </summary>
    /// <param name="key">PlayerPrefs key name</param>
    /// <typeparam name="T">The class type to deserialize</typeparam>
    /// <returns>An instance of deserialized class. New class if the key is not found</returns>
    public static T Read<T>(string key) where T : new()
    {
        return PlayerPrefs.HasKey(key)
            ? JsonUtility.FromJson<T>(PlayerPrefs.GetString(key))
            : new T();
    }

    /// <summary>
    /// Deserializes a class from JSON data stored on PlayerPrefs.
    /// Overwrite data in an object by reading from its JSON representation.
    /// </summary>
    /// <param name="key">PlayerPrefs key name</param>
    /// <param name="objectToOverwrite"></param>
    /// <typeparam name="T">The class type to deserialize</typeparam>
    /// <returns>An instance of deserialized class. New class if the key is not found</returns>
    public static void ReadOverwrite(string key, object objectToOverwrite)
    {
        if (!PlayerPrefs.HasKey(key)) return;

        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), objectToOverwrite);
    }

    /// <summary>
    /// Serializes a class to JSON string data and stores it on PlayerPrefs
    /// </summary>
    /// <param name="key">PlayerPrefs key name</param>
    /// <param name="data">Instance of the class to serialize</param>
    /// <typeparam name="T">The class type to serialize</typeparam>
    public static void Write<T>(string key, T data)
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
    }

    /// <summary>
    /// Deletes a PlayerPrefs entry
    /// </summary>
    /// <param name="key">PlayerPrefs key name</param>
    public static void Clear(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}