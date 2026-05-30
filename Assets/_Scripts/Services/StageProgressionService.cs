using System.Collections.Generic;
using UnityEngine;

public class StageProgressionService : MonoBehaviour
{
    [Header("XP & Stage Progress")]
    [SerializeField] private int currentXP;
    [SerializeField] private List<StageData> stages;

    private int highestUnlockedStage = 0;

    public int CurrentXP => currentXP;
    public int HighestUnlockedStage => highestUnlockedStage;

    public delegate void StageUnlockedHandler(StageData stage);
    public event StageUnlockedHandler OnStageUnlocked;

    private void Start()
    {
        LoadStages();
        CheckUnlockedStages();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        CheckUnlockedStages();
    }

    private void LoadStages()
    {
        // Optional: load dynamically from Resources if not assigned in Inspector
        if (stages == null || stages.Count == 0)
        {
            stages = new List<StageData>(Resources.LoadAll<StageData>("Stages"));
        }

        stages.Sort((a, b) => a.xpRequired.CompareTo(b.xpRequired)); // sort by XP
    }

    private void CheckUnlockedStages()
    {
        for (int i = 0; i < stages.Count; i++)
        {
            if (currentXP >= stages[i].xpRequired && i > highestUnlockedStage)
            {
                highestUnlockedStage = i;
                OnStageUnlocked?.Invoke(stages[i]);
                Debug.Log($"Stage Unlocked: {stages[i].stageName} ({stages[i].rewardName})");
            }
        }
    }

    public StageData GetCurrentStageData() => stages[highestUnlockedStage];
    public List<StageData> GetAllStages() => stages;
    public bool IsStageUnlocked(int index) => index <= highestUnlockedStage;
}
