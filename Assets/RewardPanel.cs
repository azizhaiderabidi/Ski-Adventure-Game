using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : UIScreen
{
    [SerializeField] Image rewardimage;
    public Button nextButton;


    LevelDataSO currentLevel;

    public override void SetData(object data)
    {
        if (data is LevelDataSO result)
        {
            currentLevel = result;
        }
    }

    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);

    }

    protected override void OnShow()
    {
        rewardimage.sprite = currentLevel.levelIcon;
    }


    private void OnNext()
    {
        UIManager.Instance.HidePopup();
    }
}
