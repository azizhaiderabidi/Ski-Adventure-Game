using System.Collections.Generic;
using UnityEngine;

public class UIFactory
{
    private readonly Dictionary<UIScreenSO, GameObject> _prefabs;

    public UIFactory(Dictionary<UIScreenSO, GameObject> prefabs)
    {
        _prefabs = prefabs;
    }

    public UIScreen Create(UIScreenSO screenSO, Transform parent)
    {
        if (!_prefabs.TryGetValue(screenSO, out var prefab))
        {
            Debug.LogError($"UIFactory: Prefab not found for {screenSO.name}");
            return null;
        }

        var instance = Object.Instantiate(prefab, parent);
        var screen = instance.GetComponent<UIScreen>();

        if (screen == null)
        {
            Debug.LogError($"UIFactory: No UIScreen component found on {prefab.name}");
            return null;
        }

        screen.SetData(screenSO); // Optional: pass SO to the screen
        return screen;
    }
}
