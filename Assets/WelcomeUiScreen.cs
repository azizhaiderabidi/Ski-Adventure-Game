using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeUiScreen : UIScreen
{
    public void Contneue()
    {
        UIManager.Instance.ShowScreen(UIScreenConstants.GamePlay);
    }

    public void Done()
    {
        PlayerPrefs.SetString("SelectedCharacter", "Board");
        GameManager.Instance.modelLoader.SetPlayer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
