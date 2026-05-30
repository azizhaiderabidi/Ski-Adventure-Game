using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Factory & Parents")]
    [SerializeField] private UIFactorySO uiFactory;
    [SerializeField] private Transform canvasParent;
    [SerializeField] private Transform popupParent;

    private Dictionary<string, UIScreen> _activeScreens = new();
    private UIScreen _currentScreen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        uiFactory.Initialize();

    }




    /// <summary>
    /// Show a new screen by ID. Hides the previous one.
    /// </summary>
    public void ShowScreen(string screenID, object data = null)
    {
        if (_activeScreens.TryGetValue(screenID, out var screen))
        {
            screen.SetData(data);

            // ✅ Always refresh even if screen is already active
            SwitchToScreen(screen); // Let SwitchToScreen handle showing it again
            return;
        }

        UIScreen newScreen = uiFactory.CreateScreen(screenID, canvasParent, data);
        if (newScreen == null) return;

        _activeScreens[screenID] = newScreen;
        SwitchToScreen(newScreen);
    }



    private void SwitchToScreen(UIScreen newScreen)
    {
        if (_currentScreen != null && _currentScreen != newScreen)
        {
            _currentScreen.Hide();
        }

        // ✅ Always show even if it's already current (for refresh)
        _currentScreen = newScreen;
        _currentScreen.Show(); // Even if same screen, it re-triggers Show()
    }

    UIScreen popup;
    public void ShowPopup(string screenID, object data = null)
    {
        popup = uiFactory.CreateScreen(screenID, popupParent);
        popup.SetData(data);
        popup?.Show();
    }
    public void HidePopup()
    {
        popup?.Hide();
    }
    public void CloseScreen(string screenID)
    {
        if (_activeScreens.TryGetValue(screenID, out var screen))
        {
            screen.Hide();
            Destroy(screen.gameObject, screen.animationDuration);
            _activeScreens.Remove(screenID);

            if (_currentScreen == screen)
                _currentScreen = null;
        }
    }

    public UIScreen GetCurrentScreen()
    {
        return _currentScreen;
    }

    public UIScreen GetScreen(string screenID)
    {
        return _activeScreens.TryGetValue(screenID, out var screen) ? screen : null;
    }
}
