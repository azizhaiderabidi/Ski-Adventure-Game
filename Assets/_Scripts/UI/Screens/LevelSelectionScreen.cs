using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class LevelSelectionScreen : UIScreen
{
    [Header("UI Refs")]
    [SerializeField] private Transform buttonParent;
    [SerializeField] private LevelButton levelButtonPrefab;
    // [SerializeField] UIButton backButton;
    [SerializeField] private Button back;

    [Header("Data")]
    [SerializeField] private LevelDatabaseSO database;

    private List<LevelButton> createdButtons = new();

    private void Awake()
    {
        // backButton.Init();
        back.onClick.AddListener(() => OnBack());
    }

    private void OnBack()
    {
        UIManager.Instance.ShowScreen(UIScreenConstants.ModeSelection);
    }

    public override void Init()
    {
        base.Init();
        GenerateLevelButtons();
    }

    private void GenerateLevelButtons()
    {
        for (int i = 0; i < database.levels.Count; i++)
        {
            LevelButton button = Instantiate(levelButtonPrefab, buttonParent);

            button.Setup(database.levels[i]);

            createdButtons.Add(button);
        }
    }

    public override void Show()
    {
        base.Show();
        Debug.Log("LevelSelectionScreen OnShow triggered");
        foreach (var button in createdButtons)
        {
            button.Refresh(); // Update stars/lock in case something changed
        }
    }
}
