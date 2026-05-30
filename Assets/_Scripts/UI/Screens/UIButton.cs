using System;

[Serializable]
public struct UIButton
{
    public UnityEngine.UI.Button button;

    public UIScreenSO UIType;

    public void Init()
    {
        button.onClick.AddListener(OnButtonClicked);

    }


    private void OnButtonClicked()
    {
        UIManager.Instance.ShowScreen(UIType.screenID);
        SoundManager.Instance.PlayButton();
    }
}



