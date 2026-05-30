using UnityEngine;

public static class LevelProgressManager
{
    private const string UnlockedLevelKey = "UnlockedLevel";
    private const string LevelStarsKeyPrefix = "Level_{0}_Stars";

    /// <summary>
    /// Get the highest unlocked level. Defaults to level 1.
    /// </summary>
    public static int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt(UnlockedLevelKey, 1);
    }

    /// <summary>
    /// Returns number of stars earned for a given level.
    /// </summary>
    public static int GetStars(int levelNumber)
    {
        return PlayerPrefs.GetInt(string.Format(LevelStarsKeyPrefix, levelNumber), 0);
    }

    /// <summary>
    /// Save stars earned for a level. If new stars are greater, they replace old.
    /// </summary>
    public static void SaveStars(int levelNumber, int stars)
    {
        int existingStars = GetStars(levelNumber);
        if (stars > existingStars)
        {
            PlayerPrefs.SetInt(string.Format(LevelStarsKeyPrefix, levelNumber), stars);
        }
    }

    /// <summary>
    /// Unlock the next level if it's not already unlocked.
    /// </summary>
    public static void UnlockNextLevel(int currentLevel)
    {
        int highestUnlocked = GetUnlockedLevel();
        if (currentLevel >= highestUnlocked)
        {
            PlayerPrefs.SetInt(UnlockedLevelKey, currentLevel + 1);
        }
    }

    /// <summary>
    /// Clear all level data (useful for debugging).
    /// </summary>
    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(UnlockedLevelKey);
        for (int i = 1; i <= 100; i++)
        {
            PlayerPrefs.DeleteKey(string.Format(LevelStarsKeyPrefix, i));
        }
    }
}

