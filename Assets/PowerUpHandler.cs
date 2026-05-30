using System.Collections.Generic;

public static class PowerUpHandler
{
    public static int CoinMulti = 1;
    public static int SpeedMulti = 1;
    public static bool HasMagnet = false;

    private static Dictionary<PowerUpType, Timer> activePowerUps = new Dictionary<PowerUpType, Timer>();

    public static void ActivatePowerUp(PowerUpType type)
    {
        if (activePowerUps.ContainsKey(type))
        {
            activePowerUps[type].Kill();
            activePowerUps[type] = null;
        }

        switch (type)
        {
            case PowerUpType.Coins2x:
                CoinMulti = 2;
                break;
            case PowerUpType.SpeedBoost:
                SpeedMulti = 2;
                break;
            case PowerUpType.Magnet:
                HasMagnet = true;
                break;
        }

        Timer timer = new Timer(10, () =>
        {
            switch (type)
            {
                case PowerUpType.Coins2x:
                    CoinMulti = 1;
                    break;
                case PowerUpType.SpeedBoost:
                    SpeedMulti = 1;
                    break;
                case PowerUpType.Magnet:
                    HasMagnet = false;
                    break;
            }

            if (activePowerUps.ContainsKey(type))
                activePowerUps.Remove(type);
        });

        activePowerUps[type] = timer;
    }

    public static void ResetAllPowerUps()
    {
        foreach (var timer in activePowerUps.Values)
            timer.Kill();

        activePowerUps.Clear();

        CoinMulti = 1;
        SpeedMulti = 1;
        HasMagnet = false;
    }

    public static bool HasPowerUp(PowerUpType type)
    {
        return activePowerUps.ContainsKey(type);
    }

    public static Timer GetTimerOfPowerUp(PowerUpType type)
    {
        if (activePowerUps.TryGetValue(type, out Timer timer))
        {
            return timer;
        }
        return null;
    }
}
