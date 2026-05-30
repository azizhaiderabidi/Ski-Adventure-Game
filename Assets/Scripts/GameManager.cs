using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using Dreamteck.Forever;
using static GameEvents;
using UnityEngine.UIElements;
using System;
using Unity.VisualScripting;


public enum GameMode
{
    None,
    LevelMode,
    Endless
}
public class GameManager : MonoBehaviour
{

    [SerializeField] Hazard snowHazard;
    [SerializeField] CameraFollow followCam;
    [SerializeField] public GameObject player;
    public Transform lastPlayerPos;
    public GameMode GameMode = GameMode.None;

    private float startZ;
    public int coins = 0;
    public float score = 0f;
    public float comboScore = 50f;
    public bool isGameActive = false;
    private string scoreString;

    public float scoreThreshold = 1f; // Increase score every 1 unit
    private float lastScoredZ;
    public LevelDatabaseSO LevelDatabase;

    public static GameManager Instance { get; private set; }
    public bool isReady = false;
    public Material[] skyboxes;
    public Color[] colors;
    public static bool IsRestartRequired = false;
    public static int currentLevelCharacterIndex;
    public LevelSegment currentSegment;

    public GameObject[] gameplaySceneObjects;
    [SerializeField] public ModelLoadler modelLoader;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }


    public void ShowGamePlayScene(bool isActive)
    {

        foreach (var scene in gameplaySceneObjects)
        {
            scene.gameObject.SetActive(isActive);
        }

    }


    Timer levelSegmentSetter;
    void Start()
    {
        Debug.Log("Current Mode:->" + PlayerPrefs.GetString("mode"));
        // levelSegmentSetter = new Timer(.2f, OnLevelSegmentSet,true);


         if(PlayerPrefs.GetString("mode")=="")
        {
            modelLoader.SetPlayer();
            UIManager.Instance.ShowScreen(UIScreenConstants.MainMenu);
        }





        int sky = PlayerPrefs.GetInt("SelectedMap", 0);

        RenderSettings.skybox = skyboxes[sky];
        RenderSettings.fogColor = colors[sky];
        // Optional: If you're using a procedural skybox or directional light
        DynamicGI.UpdateEnvironment();


        if (player != null)
        {
            lastScoredZ = player.transform.position.z;
        }
    }

    public void StartTutorial()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            Debug.Log("StartTutorial");
            UIManager.Instance.ShowScreen(UIScreenConstants.Welcome);
            modelLoader.SetPlayer();
        }
        else
        {
            string mode = PlayerPrefs.GetString("mode", "none");

            if (mode == "endless")
            {
                isGameActive = true;
                modelLoader.SetPlayer();
                UIManager.Instance.ShowScreen(UIScreenConstants.GamePlay);
                Debug.Log("Restart Endless");
            }
            else if (mode == "level")
            {
                if (LevelTracker.Current)
                {
                    isGameActive = true;
                    modelLoader.LoadModel(LevelTracker.Current.levelIndex);

                    if (PlayerPrefs.GetInt("next", 0) == 1)
                    {
                        PlayerPrefs.SetInt("next", 0); // ✅ Always reset after use

                        int unlockedLevel = LevelProgressManager.GetUnlockedLevel();
                        Debug.Log("unlockedLevel: " + unlockedLevel);

                        if (unlockedLevel < LevelDatabase.levels.Count)
                        {
                            LevelTracker.Current = LevelDatabase.levels[unlockedLevel];
                            SetLevel(LevelTracker.Current);
                            modelLoader.LoadModel(LevelTracker.Current.levelIndex);
                            Debug.Log("Next Level");
                        }
                    }
                    else
                    {
                        SetLevel(LevelTracker.Current);
                        Debug.Log("Restart Level");
                        PlayerPrefs.SetString("mode", "level");
                    }
                }
            }
            else
            {
                modelLoader.SetPlayer();
                UIManager.Instance.ShowScreen(UIScreenConstants.MainMenu);
            }
        }
    }


    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("mode", "none");
        Debug.Log(PlayerPrefs.GetString("mode"));
    }
    private void OnEnable()
    {
        EventManager.Register<StopHazard>(OnHazardStop);
    }

    private void OnDisable()
    {
        EventManager.Unregister<StopHazard>(OnHazardStop);

    }
    private void OnHazardStop(StopHazard hazard)
    {

    }

    private void OnLevelSegmentSet()
    {
        Debug.Log("Initial Segment Set");
        currentSegment = FindObjectOfType<LevelGenerator>().transform.GetChild(1).GetComponent<LevelSegment>();
        currentSegment.transform.GetComponentInChildren<PlayerSegmentTracker>().SetPlayerPosition();
    }

    private void ShowLevelSelection()
    {
        UIManager.Instance.ShowScreen(UIScreenConstants.LevelSelection);
        Debug.Log("Show Level Selection");
    }
    public string GetScore()
    {
        return scoreString;
    }

    bool completed;

    void Update()
    {
        if (isGameActive && player != null)
        {
            float distanceMoved = player.transform.position.z - lastScoredZ;

            if (distanceMoved >= scoreThreshold)
            {
                int steps = Mathf.FloorToInt(distanceMoved / scoreThreshold);
                score += steps;
                lastScoredZ += steps * scoreThreshold;
                TrySetHighScore((int)score);
            }

            scoreString = "Score: " + score.ToString();

            if (!completed && LevelTracker.Current)
            {
                if (score >= LevelTracker.Current.targetDistance)
                {
                    UIManager.Instance.ShowScreen(UIScreenConstants.LevelComplete, LevelTracker.Current);
                    SoundManager.Instance.GameWin();
                    EventManager.Raise<GameEvents.StopHazard>(new StopHazard());

                    completed = true;
                }
            }
        }



    }

    public void AddComboScore(int xFactor)
    {
        score = score + (xFactor * comboScore);
    }


    public void GameOver()
    {
        SetCameraFollow();
        lastScoredZ = 0;
        SoundManager.Instance.GameOver();
    }

    public float lerpDuration = 1.5f; // Duration of the lerp
    public float smoothSpeed = 2f; // Speed multiplier for smoothing

    private Coroutine moveCoroutine;

    public void StartCameraLerp()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(SmoothLerpToTarget(snowHazard.hazard.transform.position));
    }

    private IEnumerator SmoothLerpToTarget(Vector3 targetPosition)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < lerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / lerpDuration;

            // Smoothly interpolate position
            transform.position = Vector3.Lerp(startPos, targetPosition, Mathf.SmoothStep(0, 1, t));

            yield return null;
        }

        transform.position = targetPosition;
        followCam.target = snowHazard.hazard.transform;
    }
    private void SetCameraFollow()
    {
        player.SetActive(false);
        StartCameraLerp();
        Invoke(nameof(ShowGameOverScreen), .1f);

    }

    //main game over function
    void ShowGameOverScreen()
    {
        Debug.Log("GameOver");
        // EventManager.Raise(new StopHazard());
        if (isGameActive)
        {
            UIManager.Instance.ShowScreen(UIScreenConstants.GameOver);
            EventManager.Raise<GameEvents.GameOver>(new GameEvents.GameOver(false));

            // ResetObjects();
            isGameActive = false;
        }
    }

    //void ResetObjects()
    //{
    //    new Timer(2f, () =>
    //    {
    //        snowHazard.hazard.runner.follow = false;
    //        snowHazard.hazard.transform.GetChild(0).transform.GetComponent<ParticleSystem>().Stop();
    //        snowHazard.hazard.runner.isPlayer = false;
    //        snowHazard.hazard.runner.follow = false;
    //        Runner.playerInstance = null;
    //        EventManager.Raise(new GameEvents.StopHazard());
    //    }, true);
    //}
    public void RestartLevel()
    {
        if (LevelTracker.Current != null)
        {
            // You are in level mode
            PlayerPrefs.SetString("mode", "level");
            PlayerPrefs.SetInt("next", 0);
        }
        else
        {
            // You are in endless mode
            PlayerPrefs.SetString("mode", "endless");
        }

        LoadingManager.Instance.ShowLoading();
    }



    // private void ResetHazard()
    // {
    // HazardTrigger hazard = FindObjectOfType<Hazard>().hazard.GetComponent<HazardTrigger>();
    // hazard.gameObject.SetActive(false);
    // hazard.runner.isPlayer = false;
    // Runner.playerInstance = null;
    // followCam.target = player.transform;
    // hazard.transform.GetChild(0).transform.GetComponent<ParticleSystem>().Stop();
    // player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    // player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    // player.transform.position = lastPlayerPos.position;
    // player.SetActive(true);
    // player.GetComponent<SnowboarderController>().ResetRotation();
    // UIManager.Instance.ShowScreen(UIScreenConstants.GamePlay);
    // //resetting the UI
    // EventManager.Raise(new RestartLevel());
    // EventManager.Raise(new ScoreAdded(-(int)score));
    //// EventManager.Raise(new RestartLevel());
    // new Timer(5f, () =>
    // {

    //     hazard.transform.GetChild(0).transform.GetComponent<ParticleSystem>().Stop();

    //     Runner.playerInstance = hazard.runner;
    //     if (LevelGenerator.instance != null && LevelGenerator.instance.ready)
    //     {
    //         int segmentIndex = 0;
    //         double localPercent = LevelGenerator.instance.GlobalToLocalPercent(hazard.runner.startPercent, out segmentIndex);

    //         LevelSegment currentSegment = this.currentSegment;
    //         if(currentSegment != null)
    //         {

    //         hazard.runner.StartFollow(currentSegment, localPercent);
    //         }
    //     }
    //     else
    //     {
    //         Debug.LogWarning("LevelGenerator not ready. Can't reset hazard position.");
    //     }
    //     hazard.gameObject.SetActive(true);
    //     Debug.Log("Hazard Restart");

    //     hazard.transform.GetChild(0).transform.GetComponent<ParticleSystem>().Stop();

    //     new Timer(.2f, () =>
    //     {
    //         hazard.transform.GetChild(0).transform.GetComponent<ParticleSystem>().Play();

    //     }, true);

    //     EventManager.Raise(new StartHazard());
    // }, true);


    // }

    public void SetPlayer()
    {
        modelLoader.LoadModel(currentLevelCharacterIndex);
    }
    public void Home()
    {
        LevelTracker.Current = null;
        PlayerPrefs.SetString("mode", "none");
        PlayerPrefs.SetInt("next", 0);

        LoadingManager.Instance.ShowLoading();
    }


    public void ResetPlayer()
    {
        //ResetHazard();
        isGameActive = true;
        player.SetActive(false);
        player.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.transform.position = lastPlayerPos.position;
        player.SetActive(true);
        followCam.target = player.transform;

    }
    public void Next()
    {
        PlayerPrefs.SetString("mode", "level");
        PlayerPrefs.SetInt("next", 1);

        LoadingManager.Instance.ShowLoading();
    }



    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public void SetLevel(LevelDataSO level)
    {
        LevelTracker.Current = level;
        Debug.Log("CurrentLevel:" + level.levelIndex + " unlockedLevel" + LevelProgressManager.GetUnlockedLevel());
        UIManager.Instance.ShowScreen(UIScreenConstants.LevelObjective);

        var obj = UIManager.Instance.GetCurrentScreen() as LevelObjectiveScreen;

        obj.SetUpLevelObjective($"Reach distance {LevelTracker.Current.targetDistance}m Mark.", () =>
        {
            UIManager.Instance.ShowScreen(UIScreenConstants.GamePlay);
            isGameActive = true;
            // EventManager.Raise(new StartHazard());
        });
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    // Try to set new high score if current score is higher
    public void TrySetHighScore(int newScore)
    {
        int currentHigh = GetHighScore();
        if (newScore > currentHigh)
        {
            PlayerPrefs.SetInt("HighScore", newScore);
            PlayerPrefs.Save();
            Debug.Log("New High Score: " + newScore);
        }
    }

    public void StopPlayer()
    {
        player.GetComponent<Rigidbody>().isKinematic = true;
    }
}


public static class LevelTracker
{
    public static LevelDataSO Current;
}