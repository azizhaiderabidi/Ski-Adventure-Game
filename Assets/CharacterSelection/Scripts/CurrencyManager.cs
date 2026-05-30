using UnityEngine;

public static class CurrencyManager
{
    private const string CoinKey = "PLAYER_COINS";

    public static int Coins
    {
        get => PlayerPrefs.GetInt(CoinKey, 0); // Default 1000 coins
        set
        {
            PlayerPrefs.SetInt(CoinKey, value);
            PlayerPrefs.Save();
        }
    }

    public static bool Spend(int amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            return true;
        }
        return false;
    }

    public static void Add(int amount)
    {
        Coins += amount;
    }
}
