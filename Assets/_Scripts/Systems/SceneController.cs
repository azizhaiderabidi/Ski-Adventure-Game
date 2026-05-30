using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public static class SceneController
{
    public static void LoadSceneAsync(string sceneName)
    {
      //  GameManager.Instance.StartCoroutine(LoadAsync(sceneName));
    }

    private static IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
