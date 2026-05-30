using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

[System.Serializable]
public class Snowboard
{
    public string name;
    public int price;
    public int upgradeCost;
    public int level;
    public float speed;
    public float brake;
    public float flip;

    public bool isPurchased;
    public GameObject boardObject;
    public Material[] upgradeTextures;

    public void ApplyTexture()
    {
        Renderer renderer = boardObject.GetComponent<Renderer>();
        if (renderer != null && level > 0)
        {
            renderer.material = upgradeTextures[level - 1];
        }
    }

    public void Upgrade()
    {
        if (level < upgradeTextures.Length)
        {
            level++;
            speed += 1.5f;
            brake += 1.2f;
            flip += 1.3f;
            upgradeCost += 200;
            ApplyTexture();
        }
    }
}






public class SnowboardManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image speedFillBar;
    public Image brakeFillBar;
    public Image flipFillBar;
    public TextMeshProUGUI priceText;
    public Button buyButton;
    public Button upgradeButton;

    public Transform textureButtonParent;
    public GameObject textureButtonPrefab;

    public Snowboard[] snowboards;
    public int playerCoins;
    public TextMeshProUGUI coinsText;
    private int selectedIndex = 0;

    void Start()
    {
        LoadData();
        UpdateBoardVisibility();
        UpdateUI();
    }

    public void PurchaseSnowboard()
    {
        Snowboard sb = snowboards[selectedIndex];

        if (!sb.isPurchased && playerCoins >= sb.price)
        {
            playerCoins -= sb.price;
            sb.isPurchased = true;
            SaveData();
            UpdateUI();
        }
    }

    public void UpgradeSnowboard()
    {
        Snowboard sb = snowboards[selectedIndex];

        if (sb.isPurchased && playerCoins >= sb.upgradeCost)
        {
            playerCoins -= sb.upgradeCost;
            sb.Upgrade();
            SaveData();
            UpdateUI();
        }
    }

    public void OnNextSnowboard()
    {
        selectedIndex = (selectedIndex + 1) % snowboards.Length;
        UpdateBoardVisibility();
        UpdateUI();
    }

    public void OnPreviousSnowboard()
    {
        selectedIndex = (selectedIndex - 1 + snowboards.Length) % snowboards.Length;
        UpdateBoardVisibility();
        UpdateUI();
    }

    void UpdateBoardVisibility()
    {
        for (int i = 0; i < snowboards.Length; i++)
        {
            snowboards[i].boardObject.SetActive(i == selectedIndex);
        }
    }

    void UpdateUI()
    {
        Snowboard sb = snowboards[selectedIndex];

        nameText.text = sb.name;
        priceText.text = sb.isPurchased ? $"Upgrade Cost: {sb.upgradeCost}" : $"{sb.price}";

        buyButton.gameObject.SetActive(!sb.isPurchased);
        upgradeButton.gameObject.SetActive(sb.isPurchased);

        // Normalize the values for fill bars
        float maxSpeed = 20f;  // Max speed value for full fill
        float maxBrake = 15f;  // Max brake value for full fill
        float maxFlip = 10f;   // Max flip value for full fill

        speedFillBar.fillAmount = sb.speed / maxSpeed;
        brakeFillBar.fillAmount = sb.brake / maxBrake;
        flipFillBar.fillAmount = sb.flip / maxFlip;

        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }



    public void ChangeTexture(int index)
    {
        Snowboard sb = snowboards[selectedIndex];

        if (index < sb.upgradeTextures.Length)
        {
            Renderer renderer = sb.boardObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = sb.upgradeTextures[index];
            }
        }
    }

    void SaveData()
    {
        PlayerPrefs.SetInt("PlayerCoins", playerCoins);
        for (int i = 0; i < snowboards.Length; i++)
        {
            PlayerPrefs.SetInt($"Snowboard{i}_Purchased", snowboards[i].isPurchased ? 1 : 0);
            PlayerPrefs.SetInt($"Snowboard{i}_Level", snowboards[i].level);
        }
    }

    void LoadData()
    {
        playerCoins = PlayerPrefs.GetInt("PlayerCoins", 10000);
        for (int i = 0; i < snowboards.Length; i++)
        {
            snowboards[i].isPurchased = PlayerPrefs.GetInt($"Snowboard{i}_Purchased", 0) == 1;
            snowboards[i].level = PlayerPrefs.GetInt($"Snowboard{i}_Level", 1);
            snowboards[i].ApplyTexture();
        }
    }
}
