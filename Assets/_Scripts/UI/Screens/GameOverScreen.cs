using DG.Tweening;
using HardDev.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : UIScreen
{

    [SerializeField] Button home;
    [SerializeField] Button restart;

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] GameObject panel;

    [Header("Is Level Data")]
    [SerializeField] GameObject levelInfo;
    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI progressText;

    [Header("Is Endless Data")]
    [SerializeField] GameObject endlessInfo;
    [SerializeField] TextMeshProUGUI score , scoreAmount;
    [SerializeField] TextMeshProUGUI highScore , highScoreAmount;


    private void Awake()
    {
        home.onClick.AddListener(() => { GameManager.Instance.Home(); });
        restart.onClick.AddListener(() => { GameManager.Instance.RestartLevel(); });
    }

    private void OnEnable()
    {
        bool isLevelBased = LevelTracker.Current;


        SetupInfo(isLevelBased);
        Animate(isLevelBased);
        GameManager.Instance.StopPlayer();
    }

    void SetupInfo(bool isLevelBased)
    {
        levelInfo.SetActive(isLevelBased);
        endlessInfo.SetActive(!isLevelBased);
    }

    void Animate(bool isLevelBased)
    {

        RectTransform rectTransform = panel.transform as RectTransform;

        rectTransform.anchoredPosition = new Vector2(Screen.width * 2, 0);
        rectTransform.DOAnchorPos(Vector2.zero, 1).SetDelay(.5f).SetEase(Ease.OutCubic);

        title.text = "";


        if (isLevelBased)
        {
            title.TypeWriter("Failed", .5f, 1 + .5f);


            progressText.transform.localScale = Vector3.zero;

            progressBar.fillAmount = 0;
            progressText.text = "0%";

            progressText.transform.DOScale(Vector3.one, .25f).SetDelay(1f + +.5f + .5f).SetEase(Ease.OutCubic);


            float target = (float)GameManager.Instance.score / LevelTracker.Current.targetDistance;

            progressBar.DOFillAmount(target, 1).SetDelay(1f + .5f + .5f + .25f);

            new Timer(1f + +.5f + .5f + .25f, () =>
            { 
                Timer textTimer = new Timer(1, () => { });

                float t = 0;

                textTimer.OnTick += () => {
                    t = Mathf.Lerp(t, target, Time.deltaTime);
                    progressText.text = $"{(int)(t * 100)}%";
                };
            });
        }
        else
        {
            title.TypeWriter("GameOver", .5f, 1 + .5f);

            score.text = "";
            score.TypeWriter("Score :", .5f, 1 + .5f);
            highScore.text = "";
            highScore.TypeWriter("Hight Score :", .5f, 1 + .5f);

            scoreAmount.transform.localScale = Vector3.zero;
            highScoreAmount.transform.localScale = Vector3.zero;


            scoreAmount.transform.DOScale(Vector3.one, .2f).SetDelay(1f + 1).SetEase(Ease.OutCubic);
            highScoreAmount.transform.DOScale(Vector3.one, .25f).SetDelay(1f + 1 + .05f).SetEase(Ease.OutCubic);


            float targetScore = GameManager.Instance.score;
            scoreAmount.text = "0";

            new Timer(2.75f, () =>
            {
                Timer textTimer = new Timer(1, () => { });

                float t = 0;

                textTimer.OnTick += () => {
                    t = Mathf.Lerp(t, targetScore,3 * Time.deltaTime);
                    scoreAmount.text = $"{(int)(t)}";
                };
            });



            float targetHighScore = GameManager.Instance.GetHighScore();
            highScoreAmount.text = "0";


            new Timer(2.75f, () =>
            {
                Timer textTimer = new Timer(1, () => { });

                float t = 0;

                textTimer.OnTick += () => {
                    t = Mathf.Lerp(t, targetHighScore, 3 * Time.deltaTime);
                    highScoreAmount.text = $"{(int)(t)}";
                };
            });
        }
    }
}
