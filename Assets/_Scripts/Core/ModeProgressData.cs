using System.Collections.Generic;
using System;

[Serializable]
public class ModeProgressData
{
    public List<bool> unlockedLevels = new();
    public List<int> stars = new();

    public bool IsUnlocked(int index)
    {
        return index < unlockedLevels.Count && unlockedLevels[index];
    }

    public int GetStars(int index)
    {
        return index < stars.Count ? stars[index] : 0;
    }

    public void UnlockLevel(int index)
    {
        while (unlockedLevels.Count <= index)
            unlockedLevels.Add(false);

        unlockedLevels[index] = true;

        while (stars.Count <= index)
            stars.Add(0);
    }
}
