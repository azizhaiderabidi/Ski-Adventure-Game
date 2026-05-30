using Dreamteck.Forever;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayScreen : UIScreen
{
    [SerializeField] UIButton[] buttons;

    [SerializeField] GameObject endless;
    [SerializeField] GameObject level;

    [Header("Endless")]
    [SerializeField] TextMeshProUGUI progressText;
    [Header("Level")]
    [SerializeField] Image progressBar;
    [SerializeField] GameObject pause;

    [Header("Player UI")]
    [SerializeField] GameObject hazardWarning;
    public Button accelerateButton;

    [Header("Tutorial UI")]
    public TextMeshProUGUI TutorialStepText;
    public GameObject forward;
    public GameObject backward;
    public GameObject jump;
    public GameObject swipe;
    private void Awake()
    {

        if (PlayerPrefs.GetInt("Tutorial", 0) == 1)
        {
            forward.SetActive(false);
            backward.SetActive(false);
            jump.SetActive(false);
            swipe.SetActive(false);
            TutorialStepText.gameObject.SetActive(false);
        }
        else
        {
            //disbale for starting first step
            forward.SetActive(false);
            backward.SetActive(false);
            jump.SetActive(false);
            swipe.SetActive(false);

            //start first step
            GetComponent<TutorialManager>().StartTutorial();
        }

        foreach (var item in buttons)
        {
            item.Init();
        }

        buttons[0].button.onClick.AddListener(() => Time.timeScale = 0f);
        //accelerateButton.onClick.AddListener(() => {
        //    EventManager.Raise(new GameEvents.StartHazard());
        //    accelerateButton.onClick.RemoveAllListeners();
        //    });
        GameObject.FindObjectOfType<ProjectedPlayer>(true).GetComponent<Rigidbody>().isKinematic = false;

    }

    void OnHazardStateUpdted(GameEvents.HazardState state)
    {
        hazardWarning.SetActive(state.IsClose);
    }

   
    private void OnEnable()
    {
        //if (GameManager.Instance.GameMode == GameMode.Endless)
        //{
        //    endless.SetActive(true);
        //    level.SetActive(false);
        //    Debug.Log("set endless UI");
        //    //Invoke(nameof(StartHazard), 2f);
        //}
        //else if (GameManager.Instance.GameMode == GameMode.LevelMode)
        //{
        //    endless.SetActive(false);
        //    level.SetActive(true);
        //    Debug.Log("set level UI");

        //}

        if(PlayerPrefs.GetInt("Tutorial",0) ==0)
        {

        }
        else
        {


            if (LevelTracker.Current)
            {
                // isLevel Based;
                endless.SetActive(false);
                level.SetActive(true);

            }
            else
            {
                // is endless;
                endless.SetActive(true);
                level.SetActive(false);
            }
            EventManager.Raise(new GameEvents.StartHazard());
            //hazardWarning.gameObject.SetActive(true);
        }


         EventManager.Register<GameEvents.HazardState>(OnHazardStateUpdted);
         EventManager.Register<GameEvents.RestartLevel>(OnRestartLevel);

    }

    void StartHazard()
    {
        
    }
    private void OnRestartLevel(GameEvents.RestartLevel level)
    {
       // Debug.LogError("tesdgafdgadfga");
        progressBar.fillAmount = 0f;
        GameManager.Instance.score = 0;
        progressText.text = GameManager.Instance.score.ToString()+"m";

    }

    private void OnDisable()
    {
        EventManager.Unregister<GameEvents.HazardState>(OnHazardStateUpdted);
        EventManager.Unregister<GameEvents.RestartLevel>(OnRestartLevel);


    }

    private void LateUpdate()
    {
        if(GameManager.Instance.isGameActive == true)
        {
          //  progressBar.transform.parent.gameObject.SetActive(LevelTracker.Current);

            if (LevelTracker.Current)
            {
                float target = (float)GameManager.Instance.score / LevelTracker.Current.targetDistance;

                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, target, 5 * Time.deltaTime);
            }
            progressText.text = $"{GameManager.Instance.score}m";
        }
        


        //playerUI.SetActive(RuntimePengun.inActive == null);
    }
}
