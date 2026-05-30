using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] TextMeshProUGUI currentText;
    [SerializeField] TextMeshProUGUI targetText;
    [SerializeField] TextMeshProUGUI levelText;

    private void OnEnable()
    {
        //bar.fillAmount = GameManager.Instance.xpSystem.GetXP() / (float)GetMax(GameManager.Instance.xpSystem.GetXP());

        //currentText.text = $"{GameManager.Instance.xpSystem.GetXP()} Xp";
        //targetText.text = $"{GetMax(GameManager.Instance.xpSystem.GetXP())} Xp";
        //levelText.text = $"Level {GetStageIndex(GameManager.Instance.xpSystem.GetXP())}.";
    }

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
    private int GetMax(int xp)
    {
        int _ret = 1600;

        if (xp < 1600) _ret = 1600;
        if (xp < 1400) _ret = 1400;
        if (xp < 1000) _ret = 1000;
        if (xp < 750) _ret = 750;
        if (xp < 500) _ret = 500;
        if (xp < 250) _ret = 250;
        if (xp < 100) _ret = 100;


        return _ret;
    }
}
