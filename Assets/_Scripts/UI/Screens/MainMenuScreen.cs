using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : UIScreen
{
    [SerializeField] CharacterDatabase characterDatabase , boardDatabase;

    [SerializeField]
    UIButton[] buttons;
    [SerializeField] Button logOut;

    private void Awake()
    {
        foreach (var item in buttons)
        {
            item.Init();
        }
    }

    private void OnEnable()
    {
        logOut.onClick.AddListener(() => LogOut());

    }

    void LogOut()
    {
        //FirebaseManager.instance.SignOut();

    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://theaniletes.com/pages/privacy-policy");
    }
    private void OnDisable()
    {
        // logOut.onClick.RemoveListener(() => FirebaseManager.instance.SignOut());

    }
    public void CharacterSelection()
    {
        CharacterSelectUI.characterDB = characterDatabase;
        CharacterSelectUI.seleciton = 0;
        LoadingManager.Instance.StartFakeLoading(1f, () =>
        {

            SceneManager.LoadScene("CharacterSelection", LoadSceneMode.Additive);
            GameManager.Instance.ShowGamePlayScene(false);
        });
        

    }
    public void BoardSelection()
    {
        CharacterSelectUI.characterDB = boardDatabase;
        CharacterSelectUI.seleciton = 1;

        LoadingManager.Instance.StartFakeLoading(1f, () =>
        {

            SceneManager.LoadScene("CharacterSelection", LoadSceneMode.Additive);
            GameManager.Instance.ShowGamePlayScene(false);
        });

    }

    public void Map()
    {
        SceneManager.LoadScene("MapSelection");
    }
    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

}

