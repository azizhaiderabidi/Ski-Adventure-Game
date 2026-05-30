using UnityEngine;

public static class SkinUnlockSystem
{
    private const string SkinKey = "SKIN_UNLOCKED_";

    public static bool IsSkinUnlocked(string characterName, string skinName, bool defaultUnlocked)
    {
        string key = $"{SkinKey}{characterName}_{skinName}";
        return PlayerPrefs.GetInt(key, defaultUnlocked ? 1 : 0) == 1;
    }

    public static void UnlockSkin(string characterName, string skinName)
    {
        string key = $"{SkinKey}{characterName}_{skinName}";
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }
}
