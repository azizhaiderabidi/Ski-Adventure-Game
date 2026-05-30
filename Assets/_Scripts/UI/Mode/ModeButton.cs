using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ModeButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button button;

    private ModeDataSO modeData;

    public void Setup(ModeDataSO data, Action<ModeDataSO> onClick)
    {
        modeData = data;

        icon.sprite = data.icon;
        title.text = data.displayName;
        

        button.onClick.RemoveAllListeners();
        
        button.onClick.AddListener(() => 
        { 
            onClick?.Invoke(modeData); 
            SoundManager.Instance.PlayButton(); 
        });
    }
}
