// _Scripts/Systems/SaveManager.cs
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private ISaveService saveService;
    private const string PlayerDataKey = "PLAYER_DATA";

    private void Awake()
    {
        saveService = new PlayerPrefsSaveService(); // Can later inject JsonFileSave or CloudSave
    }

    private void OnEnable()
    {
        EventManager.Register<XPData<PlayerXPController>>(OnXPChanged);
    }

    private void OnDisable()
    {
        EventManager.Unregister<XPData<PlayerXPController>>(OnXPChanged);
    }

    private void OnXPChanged(XPData<PlayerXPController> data)
    {
        var currentData = LoadPlayer();
        currentData.xp = data.XP;
        SavePlayer(currentData);
    }

    public void SavePlayer(PlayerData data)
    {
        saveService.Save(PlayerDataKey, data);
        Debug.Log("[SaveManager] Player data saved.");
    }

    public PlayerData LoadPlayer()
    {
        return saveService.Load(PlayerDataKey, new PlayerData());
    }

    public void DeletePlayerData()
    {
        saveService.Delete(PlayerDataKey);
        Debug.Log("[SaveManager] Player data deleted.");
    }

    public bool IsSaveAvailable()
    {
        return saveService.HasKey(PlayerDataKey);
    }
}
