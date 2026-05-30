using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

[System.Serializable]
public class CharacterSkin
{
    public string skinName;
    public Material skinMaterial;
    public Sprite skin;
    public int price;
    public bool isUnlocked;
}

[System.Serializable]
public class Character
{
    public string characterName;
    [TextArea(5, 10)]
    public string characterStory;
    public GameObject characterModel;
    public int price;
    public bool isUnlocked;
    public int selectedSkinIndex;
    public CharacterSkin[] skins;
}

[System.Serializable]
public class SavedCharacterData
{
    public bool isUnlocked;
    public int selectedSkinIndex;
    public List<bool> skinUnlockStatuses = new List<bool>();
}

[System.Serializable]
public class SavedGameData
{
    public List<SavedCharacterData> characters = new List<SavedCharacterData>();
}

public class CharacterSelectionManager : MonoBehaviour
{
    public Character[] characters;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI characterStoryText;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI coinsText;

    public GameObject skinButtonPrefab;
    public Transform skinButtonParent;

    public Button leftButton;
    public Button rightButton;
    public Button selectButton;
    public Button unlockButton;
    public Button backButton;
    public TextMeshProUGUI unlockButtonText;

    public int currentCharacterIndex = 0;
    private List<GameObject> skinButtonInstances = new List<GameObject>();

    //void Start()
    //{
    //    LoadSavedSelection(); // Load all saved selections
    //    UpdateUI();

    //    backButton.onClick.AddListener(BackButtonFunction);
    //}

    //public void NextCharacter()
    //{
    //    currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;
    //    UpdateUI();
    //}

    //public void PreviousCharacter()
    //{
    //    currentCharacterIndex = (currentCharacterIndex - 1 + characters.Length) % characters.Length;
    //    UpdateUI();
    //}

    //void UpdateUI()
    //{
    //    Character currentCharacter = characters[currentCharacterIndex];

    //    foreach (var character in characters)
    //        character.characterModel.SetActive(false);

    //    currentCharacter.characterModel.SetActive(true);

    //    characterNameText.text = currentCharacter.characterName;
    //    characterStoryText.text = currentCharacter.characterStory;
    //    priceText.text = currentCharacter.price.ToString();

    //    UpdateCoinsUI();
    //    UpdateCharacterButtons();
    //    GenerateSkinButtons();
    //}

    //void UpdateCharacterButtons()
    //{
    //    Character currentCharacter = characters[currentCharacterIndex];

    //    if (currentCharacter.isUnlocked)
    //    {
    //        unlockButton.gameObject.SetActive(false);
    //        selectButton.gameObject.SetActive(true);
    //        SetSelectButtonState(IsCharacterSelected(currentCharacterIndex));
    //    }
    //    else
    //    {
    //        selectButton.gameObject.SetActive(false);
    //        unlockButton.gameObject.SetActive(true);
    //        unlockButtonText.text = "Unlock " + currentCharacter.price;
    //    }
    //}

    //void SetSelectButtonState(bool isSelected)
    //{
    //    TextMeshProUGUI buttonText = selectButton.GetComponentInChildren<TextMeshProUGUI>();
    //    buttonText.text = isSelected ? "Selected" : "Select";
    //    selectButton.interactable = !isSelected;
    //}

    //void GenerateSkinButtons()
    //{
    //    foreach (var button in skinButtonInstances)
    //        Destroy(button);
    //    skinButtonInstances.Clear();

    //    Character currentCharacter = characters[currentCharacterIndex];

    //    for (int i = 0; i < currentCharacter.skins.Length; i++)
    //    {
    //        GameObject newButton = Instantiate(skinButtonPrefab, skinButtonParent);
    //        skinButtonInstances.Add(newButton);
    //        newButton.transform.GetChild(1).GetComponent<Image>().sprite = currentCharacter.skins[i].skin;

    //        int skinIndex = i;
    //        Button button = newButton.GetComponent<Button>();
    //        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

    //        button.onClick.RemoveAllListeners();

    //        if (currentCharacter.isUnlocked) // Only allow skin selection for unlocked characters
    //        {
    //            button.onClick.AddListener(() => SelectSkin(skinIndex));
    //            button.interactable = true;

    //            buttonText.text = currentCharacter.skins[i].isUnlocked ? "Skin " + (i + 1) : "Lock " + currentCharacter.skins[i].price;
    //        }
    //        else
    //        {
    //            button.interactable = false;
    //            buttonText.text = "Locked";
    //        }
    //    }
    //}

    //public void SelectCharacter()
    //{
    //   // ES3.Save("SelectedCharacter", currentCharacterIndex);
    //   // ES3.Save("SelectedCharacterSkin", characters[currentCharacterIndex].selectedSkinIndex);
    //    UpdateUI();
    //}

    //public void UnlockCharacter()
    //{
    //    if (SpendCoins(characters[currentCharacterIndex].price))
    //    {
    //        characters[currentCharacterIndex].isUnlocked = true;
    //        SaveUnlockData();
    //        SelectCharacter();
    //    }
    //    else
    //    {
    //        Debug.Log("Not enough coins to unlock this character.");
    //    }
    //}

    //void SelectSkin(int skinIndex)
    //{
    //    Character character = characters[currentCharacterIndex];
    //    CharacterSkin skin = character.skins[skinIndex];

    //    if (!skin.isUnlocked)
    //    {
    //        if (SpendCoins(skin.price))
    //        {
    //            skin.isUnlocked = true;
    //            SaveUnlockData();
    //        }
    //        else
    //        {
    //            Debug.Log("Not enough coins.");
    //            return;
    //        }
    //    }

    //    character.selectedSkinIndex = skinIndex;
    //    ApplySkin(currentCharacterIndex, skinIndex);

    //    ES3.Save("SelectedCharacterSkin", skinIndex);
    //    UpdateUI();
    //}

    //void ApplySkin(int characterIndex, int skinIndex)
    //{
    //    Renderer renderer = characters[characterIndex].characterModel.GetComponentInChildren<Renderer>();
    //    if (renderer != null)
    //    {
    //        renderer.material = characters[characterIndex].skins[skinIndex].skinMaterial;
    //    }
    //}

    //bool IsCharacterSelected(int index)
    //{
    //    return ES3.KeyExists("SelectedCharacter") && ES3.Load<int>("SelectedCharacter") == index;
    //}

    //bool SpendCoins(int amount)
    //{
    //    int coins = PlayerPrefs.GetInt("Coins", 10000);
    //    if (coins >= amount)
    //    {
    //        coins -= amount;
    //        PlayerPrefs.SetInt("Coins", coins);
    //        PlayerPrefs.Save();
    //        UpdateCoinsUI();
    //        return true;
    //    }
    //    return false;
    //}

    //void UpdateCoinsUI()
    //{
    //    int coins = PlayerPrefs.GetInt("Coins", 10000);
    //    coinsText.text = coins.ToString();
    //}

    //void SaveUnlockData()
    //{
    //    SavedGameData savedData = new SavedGameData();

    //    foreach (var character in characters)
    //    {
    //        SavedCharacterData charData = new SavedCharacterData
    //        {
    //            isUnlocked = character.isUnlocked,
    //            selectedSkinIndex = character.selectedSkinIndex,
    //        };

    //        foreach (var skin in character.skins)
    //        {
    //            charData.skinUnlockStatuses.Add(skin.isUnlocked);
    //        }

    //        savedData.characters.Add(charData);
    //    }

    //    ES3.Save("SavedCharacters", savedData);
    //}

    //void LoadUnlockData()
    //{
    //    if (!ES3.KeyExists("SavedCharacters")) return;

    //    SavedGameData savedData = ES3.Load<SavedGameData>("SavedCharacters");

    //    for (int i = 0; i < characters.Length; i++)
    //    {
    //        if (i < savedData.characters.Count)
    //        {
    //            characters[i].isUnlocked = savedData.characters[i].isUnlocked;
    //            characters[i].selectedSkinIndex = savedData.characters[i].selectedSkinIndex;

    //            for (int j = 0; j < characters[i].skins.Length; j++)
    //            {
    //                if (j < savedData.characters[i].skinUnlockStatuses.Count)
    //                {
    //                    characters[i].skins[j].isUnlocked = savedData.characters[i].skinUnlockStatuses[j];
    //                }
    //            }
    //        }
    //    }
    //}

    //void LoadSavedSelection()
    //{
    //    LoadUnlockData();

    //    currentCharacterIndex = ES3.KeyExists("SelectedCharacter") ? ES3.Load<int>("SelectedCharacter") : 0;
    //    characters[currentCharacterIndex].selectedSkinIndex = ES3.KeyExists("SelectedCharacterSkin")
    //        ? ES3.Load<int>("SelectedCharacterSkin")
    //        : characters[currentCharacterIndex].selectedSkinIndex;

    //    ApplySkin(currentCharacterIndex, characters[currentCharacterIndex].selectedSkinIndex);
    //    UpdateUI();
    //}

    //public void BackButtonFunction()
    //{
    //    SelectCharacter();
    //}
}
