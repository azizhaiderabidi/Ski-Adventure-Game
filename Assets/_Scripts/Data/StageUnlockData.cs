[System.Serializable]
public class StageUnlockData
{
    public int StageIndex;
    public int RequiredXP;
    public bool IsUnlocked;

    public StageUnlockData(int index, int requiredXP, bool isUnlocked = false)
    {
        StageIndex = index;
        RequiredXP = requiredXP;
        IsUnlocked = isUnlocked;
    }
}
