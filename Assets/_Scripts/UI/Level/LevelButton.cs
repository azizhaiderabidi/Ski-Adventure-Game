using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelButton : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text levelNumberText;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject[] starIcons;
    [SerializeField] private Button button;

    private LevelDataSO level;

    public void Setup(LevelDataSO level)
    {
        this.level = level;
        levelNumberText.text = this.level.levelName;
        icon.sprite = level.levelIcon;
        Refresh();

        button.onClick.AddListener(() =>
        {
            Debug.Log($"Level {this.level.levelIndex} selected!");
            GameManager.Instance.SetLevel(this.level);

            SoundManager.Instance.PlayButton();
            SoundManager.Instance.UpdateMusicVolume(0.5f);
            GameManager.currentLevelCharacterIndex = this.level.levelIndex;
            GameManager.Instance.modelLoader.LoadModel(this.level.levelIndex);
        });
    }

    public void Refresh()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        bool isUnlocked = level.levelIndex < unlockedLevel;

        lockIcon.SetActive(!isUnlocked);
        button.interactable = isUnlocked;


        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].SetActive(level.IsCompleted);
        }
    }
}
