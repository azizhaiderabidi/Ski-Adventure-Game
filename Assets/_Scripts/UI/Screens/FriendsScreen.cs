using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsScreen : UIScreen
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

    private void Start()
    {
        //FirebaseManager.instance.SetFriendSystem();
    }
}
