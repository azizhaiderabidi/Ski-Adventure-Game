using System;
using System.Collections.Generic;

[Serializable]
public class GameProgressData
{
    public Dictionary<string, List<LevelProgress>> modeProgress = new();
}

[Serializable]
public class LevelProgress
{
    public bool isUnlocked;
    public int stars;
}
