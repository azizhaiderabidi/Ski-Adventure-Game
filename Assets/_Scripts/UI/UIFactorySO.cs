using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/UIFactory")]
public class UIFactorySO : ScriptableObject
{
    [Header("All Registered Screens")]
    public List<UIScreenSO> screens;

    private Dictionary<string, UIScreenSO> _screenLookup;

    public void Initialize()
    {
        _screenLookup = new Dictionary<string, UIScreenSO>();
        foreach (var screenSO in screens)
        {
            if (!_screenLookup.ContainsKey(screenSO.screenID))
            {
                _screenLookup.Add(screenSO.screenID, screenSO);
            }
            else
            {
                Debug.LogWarning($"Duplicate screenID: {screenSO.screenID}");
            }
        }
    }

    public UIScreen CreateScreen(string screenID, Transform parent, object data = null)
    {
        if (_screenLookup == null) Initialize();

        if (_screenLookup.TryGetValue(screenID, out var screenSO))
        {
            GameObject screenGO = Instantiate(screenSO.screenPrefab, parent);
            screenGO.GetComponent<UIScreen>().Init();
            screenGO.name = $"UI_{screenSO.screenID}";
            var screen = screenGO.GetComponent<UIScreen>();

            if (screen != null && data != null)
            {
                screen.SetData(data);
            }

            return screen;
        }

        Debug.LogError($"Screen with ID '{screenID}' not found.");
        return null;
    }

}
