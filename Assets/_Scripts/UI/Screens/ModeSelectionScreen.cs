using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelectionScreen : UIScreen
{
    [SerializeField] private ModeDatabaseSO modeDatabase;
    [SerializeField] private Transform contentParent;
    [SerializeField] private ModeButton modeButtonPrefab;
    [SerializeField]
    UIButton[] buttons;
    private readonly List<ModeButton> spawnedButtons = new();

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        foreach (var item in buttons)
        {
            item.Init();
        }

        GenerateButtons();
    }


    private void GenerateButtons()
    {
        // Clear previous buttons
        foreach (var btn in spawnedButtons)
            Destroy(btn.gameObject);

        spawnedButtons.Clear();

        // Spawn buttons for each mode
        foreach (var mode in modeDatabase.modes)
        {
            var button = Instantiate(modeButtonPrefab, contentParent);
            button.Setup(mode, OnModeSelected);
            spawnedButtons.Add(button);
        }
    }

    private void OnModeSelected(ModeDataSO selectedMode)
    {
        Debug.Log($"Mode selected: {selectedMode.displayName}");

        // Save selected mode to ProgressManager
        GameProgressManager.Instance.SetSelectedMode(selectedMode);

        // Check if level-based mode
        if (selectedMode.isLevelBased && selectedMode.levelDatabase != null)
        {
          
            GameProgressManager.Instance.InitializeProgress(selectedMode);
            // Show LevelSelection screen again

            GameManager.Instance.GameMode = GameMode.LevelMode;
           // PlayerPrefs.SetString("mode", "level");

           // Debug.Log("GameMode set to LevelMode");
           // Debug.Log("Current Mode: " + PlayerPrefs.GetString("mode"));

            ShowLevelSelection();
            // Optionally reset player state
            // GameManager.Instance.ResetPlayer();
           // GameManager.Instance.ResetPlayer();

        }
        else
        {
            Debug.Log("Non-level-based mode selected. Starting mode...");

            StartNonLevelBasedMode(selectedMode);

            GameManager.Instance.GameMode = GameMode.Endless;
           // PlayerPrefs.SetString("mode", "endless");

          //  Debug.Log("GameMode set to Endless");
          //  Debug.Log("Current Mode: " + PlayerPrefs.GetString("mode"));
            SoundManager.Instance.UpdateMusicVolume(0.5f);

           // GameManager.Instance.ResetPlayer();
            GameManager.Instance.modelLoader.SetPlayer();
        }
    }

    [Button]
    private void ShowLevelSelection()
    {
        UIManager.Instance.ShowScreen(UIScreenConstants.LevelSelection);
        Debug.Log("Show Level Selection");
    }

    private void StartNonLevelBasedMode(ModeDataSO mode)
    {
        // Handle different non-level-based modes here using mode.modeID if needed
        Debug.Log($"Starting non-level-based mode: {mode.modeID}");

        // Example:
        if (mode.modeID == "Endless")
        {
            // Replace with your actual endless mode start logic
            UIManager.Instance.ShowScreen(UIScreenConstants.GamePlay);
            
            GameManager.Instance.isGameActive = true;
            // or GameManager.Instance.StartGame();
        }
        else
        {
            // Fallback/default start
            UIManager.Instance.ShowScreen(UIScreenConstants.LevelSelection);
        }
    }
}
