using UnityEngine;

public class GenericXPSystem<T> where T : class
{
    private readonly T owner;
    private readonly IXPStrategy xpStrategy;

    private int currentXP;

    public GenericXPSystem(T owner, IXPStrategy strategy = null)
    {
        this.owner = owner;
        this.xpStrategy = strategy ?? new DefaultXPStrategy();
    }

    public int GetXP() => currentXP;

    public void AddXP(int amount)
    {
        var oldXP = currentXP;
        currentXP += amount;

        EventManager.Raise(new XPData<T>(owner, currentXP));

        if (xpStrategy.HasReachedNewStage(oldXP, currentXP))
        {
            int stage = xpStrategy.GetStageIndex(currentXP);
            EventManager.Raise(new StageCompleteEvent($"[{typeof(T).Name}] reached Stage {stage} at {currentXP} XP"));
           
        }
    }

    public void SubtractXP(int amount)
    {
        currentXP = Mathf.Max(currentXP - amount, 0);
        EventManager.Raise(new XPData<T>(owner, currentXP));
    }

    public void ResetXP()
    {
        currentXP = 0;
        EventManager.Raise(new XPData<T>(owner, currentXP));
    }


    public void SetXP(int value)
    {
        currentXP = value;
        EventManager.Raise(new XPData<T>((T)owner, currentXP));
    }

}
