using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level Data")]
public class LevelDataSO : ScriptableObject
{
    public string levelName;
    public string sceneName;
    public Sprite levelIcon;

    public LevelType type;

    public int targetDistance;
    public int levelIndex;

    public bool IsCompleted
    {
        get => PlayerPrefs.GetInt($"{levelName}.IsCompleted", 0) == 1;
        set => PlayerPrefs.SetInt($"{levelName}.IsCompleted", value ? 1 : 0);
    }
}

public enum LevelType
{
    DistanceTraveled,others
}