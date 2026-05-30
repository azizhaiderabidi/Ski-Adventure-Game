using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    [Header("Loading UI")]
    public GameObject loadingScreen;
    public Image progressImage;
    public TextMeshProUGUI progressText;

    [Header("Scene Settings")]
    public string sceneToLoad = "Game"; // 👈 set default scene name

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Automatically start loading scene on Awake
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    public void ShowLoading()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneToLoad));

    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName, float fakeDuration = 3f)
    {
        if (loadingScreen != null) loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float progress = 0f;

        // Step 1: Wait for real loading to reach 90%
        while (operation.progress < 0.9f)
        {
            progress = Mathf.Clamp01(operation.progress / 0.9f);
            UpdateLoadingUI(progress/.7f);
            yield return null;
        }
        operation.allowSceneActivation = true;
        // Step 2: Real loading done, now run fake progress from 0 to 1 over duration
        float timer = 0f;

        while (timer < fakeDuration)
        {
            timer += Time.deltaTime;
            float fakeProgress = Mathf.Clamp01(timer / fakeDuration);
            UpdateLoadingUI(fakeProgress);
            yield return null;
        }

        // Step 3: Ensure it's fully filled
        UpdateLoadingUI(1f);
        
        yield return new WaitUntil(()=> GameManager.Instance.isReady);
        //operation.allowSceneActivation = true;

        loadingScreen.gameObject.SetActive(false);
        GameManager.Instance.StartTutorial();
    }

    public void RetryEndless()
    {
        StartCoroutine(Retry(sceneToLoad));
    }


    private IEnumerator Retry(string sceneName, float fakeDuration = 15f)
    {
        if (loadingScreen != null) loadingScreen.SetActive(true);

        SceneManager.LoadScene("Splash");
        yield return new WaitForSeconds(0.4f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float progress = 0f;

        // Step 1: Wait for real loading to reach 90%
        while (operation.progress < 0.9f)
        {
            progress = Mathf.Clamp01(operation.progress / 0.9f);
            UpdateLoadingUI(progress);
            yield return null;
        }
        operation.allowSceneActivation = true;
        // Step 2: Real loading done, now run fake progress from 0 to 1 over duration
        float timer = 0f;

        while (timer < fakeDuration)
        {
            timer += Time.deltaTime;
            float fakeProgress = Mathf.Clamp01(timer / fakeDuration);
            UpdateLoadingUI(fakeProgress);
            yield return null;
        }

        // Step 3: Ensure it's fully filled
        UpdateLoadingUI(1f);

        // Step 4: Wait until GameManager is ready
       yield return new WaitUntil(() => GameManager.Instance.isReady);
        // Step 5: Activate scene


        if (loadingScreen != null) loadingScreen.SetActive(false);
        GameManager.Instance.GameMode = GameMode.Endless;
        PlayerPrefs.SetString("mode", "endless");
        GameManager.Instance.isGameActive = true;
        UIManager.Instance.ShowScreen(UIScreenConstants.GamePlay);
        //EventManager.Raise(new GameEvents.StartHazard());
    }


    public void StartFakeLoading(float durationInSeconds, System.Action onComplete)
    {
        StartCoroutine(FakeLoadingRoutine(durationInSeconds, onComplete));
    }

    private IEnumerator FakeLoadingRoutine(float duration, System.Action onComplete)
    {
        if (loadingScreen != null) loadingScreen.SetActive(true);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / duration);
            UpdateLoadingUI(progress);
            yield return null;
        }

        // Ensure progress is fully filled
        UpdateLoadingUI(1f);

        // Hide loading screen
        if (loadingScreen != null) loadingScreen.SetActive(false);

        // Invoke the callback
        onComplete?.Invoke();
    }



    private void UpdateLoadingUI(float progress)
    {
        if (progressImage != null)
            progressImage.fillAmount = progress;

        if (progressText != null)
            progressText.text = Mathf.RoundToInt(progress * 100f) + "%";
    }
}
