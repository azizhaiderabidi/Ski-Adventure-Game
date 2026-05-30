using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : UIScreen
{
    [SerializeField] TextMeshProUGUI xpText;

    public float Xp { get; set; }

   

    public void Home()
    {
       // UIManager.Instance.reflexGame.SetActive(false);
        //UIManager.Instance.timeManagementGame.SetActive(false);

       // UIManager.Instance.ShowScreen(UIType.MainMenu);
    }
}
