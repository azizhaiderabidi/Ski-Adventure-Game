// _Scripts/Systems/PlayerXPController.cs
using UnityEngine;

public class PlayerXPController : MonoBehaviour, IXPSystem
{
    private int currentXP;

    public int GetXP() => currentXP;

    public void GainXP(int amount)
    {
        currentXP += amount;
        Debug.Log($"[XP] Gained: {amount} | Total: {currentXP}");

        EventManager.Raise(currentXP);

        if (HasReachedStage(currentXP))
        {
            EventManager.Raise($"You've reached a new stage at {currentXP} XP!");
            EventManager.Raise($"Stage{GetStageIndex(currentXP)}"); // Scene name
        }

        currentXP = Mathf.Clamp( currentXP, 0, int.MaxValue );
    }

    public void ResetXP()
    {
        currentXP = 0;
        EventManager.Raise(currentXP);
    }

    public void SetXP(int xp)
    {
        currentXP = xp;
        Debug.Log($"[XP] Loaded XP: {currentXP}");
        EventManager.Raise(currentXP);
    }

    private bool HasReachedStage(int xp) => GetStageIndex(xp) > 1;

    private int GetStageIndex(int xp)
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
}
