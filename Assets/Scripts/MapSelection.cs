using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MapData
{
    public string mapName;
    public Sprite mapSprite;
    public int price;
    public bool isUnlocked;
    public Texture mountainTexture;
    public Texture chunkTexture;
    public Material skybox;
}

public class MapSelection : MonoBehaviour
{
    public Image mapImage;
    public TextMeshProUGUI mapNameText;
    public TextMeshProUGUI mapPriceText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI notEnoughCoinText;
    public Button selectButton;
    public Button unLockButton;
    public Image lockImage;

    public MapData[] maps;
    public int selectedMapIndex = 0;

    public Material mountainMat;
    public Material chunkMat;

    private int lastSelectedMapIndex;



    private void Start()
    {

        foreach (var button in FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            button.onClick.AddListener(SoundManager.Instance.PlayButton);
        }

        LoadMapData();

        // Get the last selected map index, fallback to 0
        lastSelectedMapIndex = PlayerPrefs.GetInt("SelectedMap", 0);
        // Make sure it’s unlocked; if not, fallback to first unlocked
        if (!maps[lastSelectedMapIndex].isUnlocked)
            lastSelectedMapIndex = GetFirstUnlockedMapIndex();

        selectedMapIndex = lastSelectedMapIndex;

        UpdateUI();
        ApplyFinalTextures(); // Apply selected map’s textures

        ApplyPreviewTextures();
    }

    public void NextMap()
    {
        selectedMapIndex = (selectedMapIndex + 1) % maps.Length;
        AnimateTextChange();
        UpdateUI();
        ApplyPreviewTextures();
    }

    public void PreviousMap()
    {
        selectedMapIndex = (selectedMapIndex - 1 + maps.Length) % maps.Length;
        AnimateTextChange();
        UpdateUI();
        ApplyPreviewTextures();
    }

    public void UnlockMap()
    {
        notEnoughCoinText.gameObject.SetActive(false);
        int playerCoins = CurrencyManager.Coins;

        if (playerCoins >= maps[selectedMapIndex].price)
        {
            CurrencyManager.Spend(maps[selectedMapIndex].price);
            maps[selectedMapIndex].isUnlocked = true;
            PlayerPrefs.SetInt("MapUnlocked_" + selectedMapIndex, 1);
            UpdateUI();
            ApplyFinalTextures(); // Unlock and apply
        }
        else
        {
            notEnoughCoinText.gameObject.SetActive(true);
            notEnoughCoinText.text = "Not Enough Coins";
        }

        coinText.text = CurrencyManager.Coins.ToString();
    }

    public void SelectMap()
    {
        if (!maps[selectedMapIndex].isUnlocked)
        {
            Debug.Log("Map is locked!");
            return;
        }

        PlayerPrefs.SetInt("SelectedMap", selectedMapIndex);
        lastSelectedMapIndex = selectedMapIndex;
        UpdateUI();
        ApplyFinalTextures(); // Save and apply
    }

    private void UpdateUI()
    {
        mapImage.sprite = maps[selectedMapIndex].mapSprite;

        mapPriceText.text = maps[selectedMapIndex].isUnlocked ? "" : "Price: " + maps[selectedMapIndex].price;
        mapNameText.text = maps[selectedMapIndex].mapName;

        lockImage.gameObject.SetActive(!maps[selectedMapIndex].isUnlocked);
        selectButton.gameObject.SetActive(maps[selectedMapIndex].isUnlocked);
        unLockButton.gameObject.SetActive(!maps[selectedMapIndex].isUnlocked);

        coinText.text = CurrencyManager.Coins.ToString();
        notEnoughCoinText.gameObject.SetActive(false);

        int savedMapIndex = PlayerPrefs.GetInt("SelectedMap");
        selectButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            selectedMapIndex == savedMapIndex ? "Selected" : "Select";
    }

    private void LoadMapData()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].isUnlocked = PlayerPrefs.GetInt("MapUnlocked_" + i, i == 0 ? 1 : 0) == 1;
        }
    }

    private int GetFirstUnlockedMapIndex()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            if (maps[i].isUnlocked) return i;
        }
        return 0; // fallback safety
    }

    private void AnimateTextChange()
    {
        mapNameText.DOFade(0, 0.2f).OnComplete(() =>
        {
            mapNameText.text = maps[selectedMapIndex].mapName;
            mapNameText.DOFade(1, 0.2f);
        });

        mapPriceText.DOFade(0, 0.2f).OnComplete(() =>
        {
            mapPriceText.text = maps[selectedMapIndex].isUnlocked ? "" : "Price: " + maps[selectedMapIndex].price;
            mapPriceText.DOFade(1, 0.2f);
        });

        mapImage.transform.DOKill();
        mapImage.transform.localScale = Vector3.one * 0.9f;
        mapImage.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack);
    }

    private void ApplyPreviewTextures()
    {
        // Preview for navigation, even if locked
        mountainMat.mainTexture = maps[selectedMapIndex].mountainTexture;
        chunkMat.mainTexture = maps[selectedMapIndex].chunkTexture;

        RenderSettings.skybox = maps[selectedMapIndex].skybox;
        // Optional: If you're using a procedural skybox or directional light
        DynamicGI.UpdateEnvironment();
    }

    private void ApplyFinalTextures()
    {
        // Apply saved map (only unlocked)
        if (maps[selectedMapIndex].isUnlocked)
        {
            mountainMat.mainTexture = maps[selectedMapIndex].mountainTexture;
            chunkMat.mainTexture = maps[selectedMapIndex].chunkTexture;
        }
    }

    public void BackButton()
    {
        ApplyFinalTextures();
        LoadingManager.Instance.ShowLoading();
        //SceneManager.LoadScene(0);
    }
}
