using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardScreen : UIScreen
{
    [SerializeField]
    UIButton[] buttons;


    private void Awake()
    {
        foreach (var item in buttons)
        {
            item.Init();
        }
    }
}
