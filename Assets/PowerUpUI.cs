using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    [SerializeField] GameObject Coin, Speed, Magnet;
    [SerializeField] TextMeshProUGUI CoinT,SpeedT,MagnetT;
    void Update()
    {
        bool hasCoin = PowerUpHandler.HasPowerUp(PowerUpType.Coins2x);
        bool hasSpeed = PowerUpHandler.HasPowerUp(PowerUpType.SpeedBoost);
        bool hasMagnet = PowerUpHandler.HasPowerUp(PowerUpType.Magnet);

        Coin.SetActive(hasCoin);
        Speed.SetActive(hasSpeed);
        Magnet.SetActive(hasMagnet);

        var coinTimer = PowerUpHandler.GetTimerOfPowerUp(PowerUpType.Coins2x);
        var speedTimer = PowerUpHandler.GetTimerOfPowerUp(PowerUpType.SpeedBoost);
        var magnetTimer = PowerUpHandler.GetTimerOfPowerUp(PowerUpType.Magnet);

        CoinT.text = hasCoin ? $"{(int)(coinTimer?.StopAt - coinTimer?.Time)}s" : "";
        SpeedT.text = hasSpeed ? $"{(int)(speedTimer?.StopAt - speedTimer?.Time)}s" : "";
        MagnetT.text = hasMagnet ? $"{(int)(magnetTimer?.StopAt - magnetTimer?.Time)}s" : "";
    }
}
