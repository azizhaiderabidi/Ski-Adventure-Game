using UnityEngine;
using System.Collections;

public class InternetChecker : MonoBehaviour
{
    [Header("UI Panel for No Internet")]
    public GameObject noInternetPanel;

    [Header("Check Interval (in seconds)")]
    public float checkInterval = 5f;

    private void Start()
    {
        StartCoroutine(CheckInternetRoutine());
    }

    IEnumerator CheckInternetRoutine()
    {
        while (true)
        {
            Debug.Log("internet Checking");
            yield return CheckInternetConnection();
            yield return new WaitForSeconds(checkInterval);
        }
    }

    IEnumerator CheckInternetConnection()
    {
        using (WWW www = new WWW("https://www.google.com"))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                noInternetPanel.SetActive(false);
                Time.timeScale = 1f; // Resume gameplay
            }
            else
            {
                noInternetPanel.SetActive(true);
                noInternetPanel.transform.SetAsLastSibling();
                Time.timeScale = 0f; // Pause gameplay
            }

        }
    }
}
