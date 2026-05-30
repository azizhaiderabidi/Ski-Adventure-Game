using UnityEngine;
using UnityEngine.UI;

public class SettingManager : UIScreen
{
    [Header("Music")]
    public Button musicButton;
    public Image musicImage;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public AudioSource musicSource;

    [Header("Sound")]
    public Button soundButton;
    public Image soundImage;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    //public AudioSource soundSource;

    private bool isMusicOn = true;
    private bool isSoundOn = true;

    void Start()
    {
        musicButton.onClick.AddListener(ToggleMusic);
        soundButton.onClick.AddListener(ToggleSound); 
        

        musicButton.onClick.AddListener(SoundManager.Instance.PlayButton);
        soundButton.onClick.AddListener(SoundManager.Instance.PlayButton);

        
        UpdateUI();
    }

    public void Back()
    {
        //LoadingManager.Instance.ShowLoading();
        UIManager.Instance.ShowScreen(UIScreenConstants.MainMenu);
    }
    void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        // musicSource.mute = !isMusicOn;
        UpdateUI();
    }

    void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        // soundSource.mute = !isSoundOn;
        UpdateUI();
    }

    void UpdateUI()
    {
        musicImage.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
        soundImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;

        SoundManager.Instance.SetMusicVolume(isMusicOn ? 1 : 0);
        SoundManager.Instance.SetSoundVolume(isSoundOn ? 1 : 0);
    }

    public void LogOut()
    {
       // FirebaseManager.instance.SignOut();
        UIManager.Instance.ShowScreen(UIScreenConstants.Login);
    }
}
