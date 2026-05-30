using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance { get; private set; }

    private const string ProgressKey = "GAME_PROGRESS";

    private GameProgressData currentProgress = new();

    public ModeDataSO SelectedMode { get; private set; }
    public LevelDataSO SelectedLevel { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadProgress();
    }

    public void SetSelectedMode(ModeDataSO mode)
    {
        SelectedMode = mode;
    }

    public void SetSelectedLevel(LevelDataSO level)
    {
        SelectedLevel = level;
    }

    public void InitializeProgress(ModeDataSO mode)
    {
        
        if (!currentProgress.modeProgress.ContainsKey(mode.modeID))
        {
            var levelCount = mode.levelDatabase.levels.Count;

            List<LevelProgress> levels = new();
            for (int i = 0; i < levelCount; i++)
            {
                levels.Add(new LevelProgress
                {
                    isUnlocked = (i == 0), // Only first level unlocked by default
                    stars = 0
                });
            }

            currentProgress.modeProgress[mode.modeID] = levels;
            SaveProgress();
        }
    }

    public List<LevelProgress> GetLevelProgress(ModeDataSO mode)
    {
        if (!currentProgress.modeProgress.TryGetValue(mode.modeID, out var levels))
        {
            InitializeProgress(mode);
            return currentProgress.modeProgress[mode.modeID];
        }

        return levels;
    }

    public void SetLevelStar(ModeDataSO mode, int levelIndex, int stars)
    {
        if (currentProgress.modeProgress.TryGetValue(mode.modeID, out var levels))
        {
            levels[levelIndex].stars = Mathf.Max(levels[levelIndex].stars, stars);

            // Unlock next level if exists
            if (levelIndex + 1 < levels.Count)
                levels[levelIndex + 1].isUnlocked = true;

            SaveProgress();
        }
    }

    private void SaveProgress()
    {
        string json = JsonUtility.ToJson(currentProgress);
        PlayerPrefs.SetString(ProgressKey, json);
        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        if (PlayerPrefs.HasKey(ProgressKey))
        {
            string json = PlayerPrefs.GetString(ProgressKey);
            currentProgress = JsonUtility.FromJson<GameProgressData>(json);
        }
        else
        {
            currentProgress = new GameProgressData();
        }
    }

    public void ClearProgress()
    {
        PlayerPrefs.DeleteKey(ProgressKey);
        currentProgress = new GameProgressData();
    }
}
