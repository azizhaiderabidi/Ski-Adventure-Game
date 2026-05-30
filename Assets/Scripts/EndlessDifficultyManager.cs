using UnityEngine;

public class EndlessDifficultyManager : MonoBehaviour
{
    public float startTimeScale = 1.0f;   // Game start time scale
    public float maxTimeScale = 3.0f;     // Max difficulty level
    public float timeToMaxDifficulty = 300f; // 300 seconds (5 minutes) to reach max difficulty
    public float smoothSpeed = 0.1f;      // How smooth the transition should be

    private float targetTimeScale;

    void Start()
    {
        Time.timeScale = startTimeScale;
        targetTimeScale = startTimeScale;
    }

    void Update()
    {
        // Calculate target timeScale based on time elapsed
        float progress = Mathf.Clamp01(Time.timeSinceLevelLoad / timeToMaxDifficulty);
//        Debug.Log("Game difficulty:" + progress);
        targetTimeScale = Mathf.Lerp(startTimeScale, maxTimeScale, progress);

        // Smooth transition
        Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, smoothSpeed * Time.deltaTime);
    }
}
