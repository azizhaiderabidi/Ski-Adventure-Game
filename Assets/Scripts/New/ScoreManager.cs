using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int coinCount = 0;
   
    public EventChannelSO addCoins;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;

        // Update UI or notify listeners here
    }
}