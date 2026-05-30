public interface IXPStrategy
{
    int GetStageIndex(int xp);
    bool HasReachedNewStage(int oldXP, int newXP);
}
