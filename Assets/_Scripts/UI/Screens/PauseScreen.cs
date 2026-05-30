using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : UIScreen
{
    public Button resumeButton;
    public Button menuButton;
    public Button restartButton;


    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResume);
        menuButton.onClick.AddListener(OnHome);
        restartButton.onClick.AddListener(OnRestart);
    }

    private void OnResume()
    {
        Time.timeScale = 1.0f;
        UIManager.Instance.ShowScreen(UIScreenConstants.GamePlay);
    }

    private void OnRestart()
    {
        Time.timeScale = 1.0f;

        GameManager.Instance.RestartLevel();
    }

    private void OnNext()
    {
       
    }

    private void OnHome()
    {
        Time.timeScale = 1.0f;

        GameManager.Instance.Home();
    }
}
