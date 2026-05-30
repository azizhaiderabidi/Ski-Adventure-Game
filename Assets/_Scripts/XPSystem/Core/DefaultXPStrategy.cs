public class DefaultXPStrategy : IXPStrategy
{
    public int GetStageIndex(int xp)
    {
        if (xp >= 1600) return 8;
        if (xp >= 1400) return 7;
        if (xp >= 1000) return 6;
        if (xp >= 750) return 5;
        if (xp >= 500) return 4;
        if (xp >= 250) return 3;
        if (xp >= 100) return 2;
        return 1;
    }

    public bool HasReachedNewStage(int oldXP, int newXP)
    {
        return GetStageIndex(newXP) > GetStageIndex(oldXP);
    }
}
