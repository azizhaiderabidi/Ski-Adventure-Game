using UnityEngine;

public static class CharacterUnlockSystem
{
    private const string UnlockKey = "CHAR_UNLOCKED_";

    public static bool IsCharacterUnlocked(string characterName, bool defaultUnlocked)
    {
        return PlayerPrefs.GetInt(UnlockKey + characterName, defaultUnlocked ? 1 : 0) == 1;
    }

    public static void UnlockCharacter(string characterName)
    {
        PlayerPrefs.SetInt(UnlockKey + characterName, 1);
        PlayerPrefs.Save();
    }
}
