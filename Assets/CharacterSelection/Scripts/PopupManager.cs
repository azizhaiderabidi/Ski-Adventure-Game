using UnityEngine;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel;
    public TMP_Text popupText;

    public void Show(string message, float duration = 2f)
    {
        popupText.text = message;
        popupPanel.SetActive(true);
        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), duration);
    }

    void Hide()
    {
        popupPanel.SetActive(false);
    }
}
