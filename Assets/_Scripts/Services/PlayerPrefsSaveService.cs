// _Scripts/Services/PlayerPrefsSaveService.cs
using UnityEngine;

public class PlayerPrefsSaveService : ISaveService
{
    public void Save<T>(string key, T data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public T Load<T>(string key, T defaultValue = default)
    {
        if (!PlayerPrefs.HasKey(key)) return defaultValue;

        string json = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<T>(json);
    }

    public void Delete(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
}
