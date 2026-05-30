using UnityEngine;

[CreateAssetMenu(menuName = "Game/Mode Data")]
public class ModeDataSO : ScriptableObject
{
    public string modeID;                 // e.g. "level", "endless"
    public string displayName;
    public Sprite icon;
    public bool isLevelBased;

    [Header("For Level-Based Modes")]
    public LevelDatabaseSO levelDatabase;
}
