// _Scripts/Core/GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance { get; private set; }

    [SerializeField] private EventChannelSO gameStartEventChannel;
    

    private ISaveService saveService;
    public IXPSystem xpSystem;

    private const string PlayerDataKey = "PLAYER_DATA";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveService = new PlayerPrefsSaveService();
        xpSystem = FindObjectOfType<PlayerXPController>();

        if (xpSystem == null)
        {
            Debug.LogError("PlayerXPController not found.");
            return;
        }

        LoadPlayerData();
    }

   
    private void Start()
    {
        Debug.Log("[GameManager] Raising StartGame event");
        gameStartEventChannel?.Raise();
    }

    private void LoadPlayerData()
    {
        if (saveService.HasKey(PlayerDataKey))
        {
            PlayerData data = saveService.Load(PlayerDataKey, new PlayerData());
            xpSystem.SetXP(data.xp);
        }
    }

    public void AddXP(int amount)
    {
        xpSystem.GainXP(amount);
        SaveCurrentPlayer();
    }

    public void SaveCurrentPlayer()
    {
        PlayerData data = new PlayerData
        {
            xp = xpSystem.GetXP()
        };

        saveService.Save(PlayerDataKey, data);
    }

    // Your other methods...
}
