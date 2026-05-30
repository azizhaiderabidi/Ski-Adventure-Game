using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreen : UIScreen
{
    [SerializeField] Button google;
    [SerializeField] Button TestLogin;

    private void OnEnable()
    {
        //google.onClick.AddListener(() => //FirebaseManager.instance.GoogleSignInClick());
        //TestLogin.onClick.AddListener(() => FirebaseManager.instance.LoginMyUser());

    }

    private void OnDisable()
    {
        //google.onClick.RemoveListener(() => FirebaseManager.instance.GoogleSignInClick());
        //TestLogin.onClick.RemoveListener(() => FirebaseManager.instance.LoginMyUser());
    }

}
