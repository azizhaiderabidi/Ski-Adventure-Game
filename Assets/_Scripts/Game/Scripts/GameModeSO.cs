using UnityEngine;

[CreateAssetMenu(menuName = "Game/Mode")]
public class GameModeSO : ScriptableObject
{
    public string modeName;
    public Sprite modeIcon;
    public string description;

    [Tooltip("Scene to load when this mode starts.")]
    public string sceneName;

    [Tooltip("Does this mode support level selection?")]
    public bool hasLevels;

    [Tooltip("Optional level database for this mode (if hasLevels is true)")]
    public LevelDatabaseSO levelDatabase;
}
