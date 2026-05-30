using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelObjectiveScreen : UIScreen
{
    [SerializeField] TextMeshProUGUI levelObjectiveText;
    [SerializeField] Button levelStartButton;


    public void SetUpLevelObjective(string objectiveText,Action levelStartButtonCallback)
    {
        levelObjectiveText.text = objectiveText;
        levelStartButton.onClick.AddListener(()=>  levelStartButtonCallback()); 
    }
}
