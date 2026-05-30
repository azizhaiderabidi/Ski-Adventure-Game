using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using HardDev.Utility;

public class LevelCompletionScreen : UIScreen
{
    [Header("UI References")]
    public GameObject[] starIcons; // e.g., 3 stars to enable
    public Button nextButton;
    public Button menuButton;
    public Button restartButton;
    public TMP_Text levelText;

    private LevelDataSO currentLevel;
    private int starsEarned;


    [SerializeField] TextMeshProUGUI title;

    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI progressText;


    private void Awake()
    {
        nextButton.onClick.AddListener(OnNext);
        menuButton.onClick.AddListener(OnMenu);
        restartButton.onClick.AddListener(OnRestart);
    }

    private void OnEnable()
    {
        Animate();
        EventManager.Raise(new GameEvents.StopHazard());
        GameManager.Instance.StopPlayer();

    }

    void Animate()
    {
        float delay = 0;
        title.TypeWriter("Level Completed", .5f, delay += 1);

        nextButton.transform.localScale = Vector3.zero;
        menuButton.transform.localScale = Vector3.zero;
        restartButton.transform.localScale = Vector3.zero;


        progressText.transform.localScale = Vector3.zero;

        progressBar.fillAmount = 0;
        progressText.text = "0%";

        progressText.transform.DOScale(Vector3.one, .25f).SetDelay(delay += 1f).SetEase(Ease.OutCubic);


        float target = (float)GameManager.Instance.score / LevelTracker.Current.targetDistance;

        progressBar.DOFillAmount(target, 1).SetDelay(delay += .5f);

        new Timer(delay, () =>
        {
            Timer textTimer = new Timer(1, () => { });

            float t = 0;

            textTimer.OnTick += () => {
                t = Mathf.Lerp(t, 1.25f,5 * Time.deltaTime);
                t = Mathf.Clamp01(t);
                progressText.text = $"{(int)(t * 100)}%";
            };
        });

        nextButton.transform.DOScale(Vector3.one,1).SetEase(Ease.OutBounce).SetDelay(delay += .5f);
        menuButton.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBounce).SetDelay(delay += .5f);
        restartButton.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutBounce).SetDelay(delay += .5f);
    }



    public override void SetData(object data)
    {
        if (data is LevelDataSO result)
        {
            currentLevel = result;
        }



    }

    protected override void OnShow()
    {
        Debug.Log("Show Reward");
        levelText.text = $"Level {currentLevel.levelIndex} Completed!";
        ShowStars(starsEarned);

        LevelProgressManager.SaveStars(currentLevel.levelIndex, starsEarned);

        int unlockedLevel = LevelProgressManager.GetUnlockedLevel();
        Debug.Log("Current UnlockedLevel: " + unlockedLevel);

        // ✅ Only unlock next level if player completed the current last unlocked level
        if (currentLevel.levelIndex >= unlockedLevel)
        {
            LevelProgressManager.UnlockNextLevel(currentLevel.levelIndex);
            Debug.Log("New UnlockedLevel: " + LevelProgressManager.GetUnlockedLevel());
        }

        // Wait and then show popup
        new Timer(.2f, () =>
        {
            int latestUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
            bool isUnlocked = currentLevel.levelIndex + 1 >= latestUnlocked;

            if (isUnlocked)
            {
                UIManager.Instance.ShowPopup(UIScreenConstants.RewardPanel, currentLevel);
            }
        }, true);
    }


    private void ShowStars(int count)
    {
        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].SetActive(i < count);
        }
    }


    private void OnRestart()
    {
        GameManager.Instance.RestartLevel();
    }

    private void OnNext()
    {

        GameManager.Instance.Next();
    }

    private void OnMenu()
    {
         GameManager.Instance.Home();
        //LoadingManager.Instance.StartFakeLoading(.5f, () => { 
        
        //    UIManager.Instance.ShowScreen(UIScreenConstants.MainMenu);
        
        //});
    }
}

public class LevelResult
{
    public int levelNumber;
    public int starsEarned;

    public LevelResult(int level, int stars)
    {
        levelNumber = level;
        starsEarned = stars;
    }
}

