using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class CharacterSelectUI : UIScreen
{
    [Header("Character Selection")]
    public static CharacterDatabase characterDB;
    public static int seleciton;
    public CharacterDatabase _characterDB;
    public GameObject normalCardPrefab;
    public GameObject specialCardPrefab;
    public Transform cardParent;
    public GameObject previewRoot;
    public GameObject charactersPanel;
    public GameObject boardsPanel;

    [Header("Skin Selection")]
    public Transform skinButtonContainer;
    public GameObject skinButtonPrefab;

    [Header("UI")]
    public PopupManager popupManager;
    public TMP_Text story;
    public TMP_Text characterName;
    public TMP_Text boardDetails;
    public TMP_Text coinText;
    public TMP_Text priceText;


    public Button selectedBtn;
    public Button selectBtn;
    public Button buyBtn;

    private CharacterData currentCharacter;
    private GameObject currentCharacterPreview;
    private SkinData currentSkin;

    private List<GameObject> skinButtons = new();
    private Dictionary<GameObject, SkinData> buttonSkinMap = new();

    void Start()
    {
        if(seleciton == 0)
        {
            charactersPanel.SetActive(true);
            boardsPanel.SetActive(false);
        }
        else
        {
            charactersPanel.SetActive(false);
            boardsPanel.SetActive(true);
        }
        foreach (var button in FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None)) 
        {
            button.onClick.AddListener(SoundManager.Instance.PlayButton);
        }

        _characterDB = characterDB;
        LoadLastSelectedCharacter();
        PopulateCharacterGrid();
        UpdateButtons();

    }

    void Update()
    {
        coinText.text = CurrencyManager.Coins.ToString();
    }

    
    public void Left()
    {
        List<CharacterData> characterData = characterDB.characters.ToList();
        int index = characterData.IndexOf(currentCharacter);
        //index = Mathf.Clamp(index - 1, 0, characterData.Count - 1);

        index--;

        if (index < 0)
            index = characterData.Count - 1;

        SetCurrentCharacter(characterData[index]);
        UpdateButtons();
    }

    public void Right()
    {
        int index = characterDB.characters.IndexOf(currentCharacter);
        //index = Mathf.Clamp(index + 1, 0, characterDB.characters.Count - 1);

        index++;

        if (index >= characterDB.characters.Count)
            index = 0;

        SetCurrentCharacter(characterDB.characters[index]);
        UpdateButtons();
    }

    public void Select()
    {
        PlayerPrefs.SetInt(characterDB.name + "SelectedCharacter", characterDB.characters.IndexOf(currentCharacter));
        UpdateButtons();
    }

    public void Buy()
    {
        bool unlocked = CharacterUnlockSystem.IsCharacterUnlocked(currentCharacter.characterName, currentCharacter.isUnlockedByDefault);

        if (!unlocked)
        {
            if (CurrencyManager.Spend(currentCharacter.price))
            {
                CharacterUnlockSystem.UnlockCharacter(currentCharacter.characterName);
                popupManager.Show($"Unlocked {currentCharacter.characterName}!");
                PopulateCharacterGrid();
            }
            else
            {
                popupManager.Show("Not enough coins to unlock character.");
                return;
            }
        }

        UpdateButtons();
    }

    public void SetCharacterData(CharacterDatabase characterDB)
    {
        CharacterSelectUI.characterDB = characterDB;
        LoadLastSelectedCharacter();
        PopulateCharacterGrid();
        UpdateButtons();
    }

    public void Home()
    {
        // SceneManager.LoadScene("Game");
        // LoadingManager.Instance.ShowLoading();
        LoadingManager.Instance.StartFakeLoading(1f, () =>
        {

            SceneManager.UnloadSceneAsync("CharacterSelection");

            GameManager.Instance.ShowGamePlayScene(true);
        });
    }

    void UpdateButtons()
    {

        bool unlocked =
            CharacterUnlockSystem.IsCharacterUnlocked(currentCharacter.characterName, currentCharacter.isUnlockedByDefault);
        
        
        bool isSelected = PlayerPrefs.GetInt(characterDB.name + "SelectedCharacter")
            == characterDB.characters.IndexOf(currentCharacter);


        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        buyBtn.interactable = currentCharacter.unlockRequriment <= unlockedLevel;

        buyBtn.GetComponentInChildren<TextMeshProUGUI>().text =
            currentCharacter.unlockRequriment <= unlockedLevel ?
            "Buy" :
            $"Beat \n Level {currentCharacter.unlockRequriment}";

        buyBtn.gameObject.SetActive(!unlocked);
        selectBtn.gameObject.SetActive(unlocked);
        selectedBtn.gameObject.SetActive(isSelected);
        priceText.gameObject.SetActive(!unlocked);
        priceText.text =  currentCharacter.price.ToString();
    }

    void PopulateCharacterGrid()
    {
        foreach (Transform child in cardParent)
            Destroy(child.gameObject);

        List<CharacterData> sortedList = new List<CharacterData>(characterDB.characters);
        sortedList.Sort((a, b) => b.isSpecial.CompareTo(a.isSpecial)); // special first

        foreach (CharacterData data in sortedList)
        {
            GameObject prefab = data.isSpecial ? specialCardPrefab : normalCardPrefab;
            GameObject card = Instantiate(prefab, cardParent);

            card.transform.localScale = Vector3.zero;
            ApplyRandomCardAnimation(card.transform, data.isSpecial);

            card.transform.Find("Icon").GetComponent<Image>().sprite = data.characterIcon;
            card.transform.Find("Name").GetComponent<TMP_Text>().text = data.characterName;

            bool unlocked = CharacterUnlockSystem.IsCharacterUnlocked(data.characterName, data.isUnlockedByDefault);
            card.transform.Find("Lock").gameObject.SetActive(!unlocked);
            card.transform.Find("Price").GetComponent<TMP_Text>().text = unlocked ? "" : $"{data.price}";

            card.GetComponent<Button>().onClick.AddListener(() => SetCurrentCharacter(data));
        }
    }

    void ApplyRandomCardAnimation(Transform card, bool isSpecial)
    {
        Vector3 targetScale = isSpecial ? new Vector3(1f, 1f, 1f) : Vector3.one;
        int rand = Random.Range(0, 4);

        switch (rand)
        {
            case 0:
                card.DOScale(targetScale, 0.4f).SetEase(Ease.OutBack);
                break;
            case 1:
                card.DOScale(targetScale * 1.2f, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
                    card.DOScale(targetScale, 0.2f));
                break;
            case 2:
                card.localScale = targetScale;
                card.DOPunchRotation(new Vector3(0, 0, 15), 0.4f, 8, 1);
                break;
            case 3:
                card.localScale = targetScale;
                card.DOPunchScale(Vector3.one * 0.3f, 0.4f, 8, 1);
                break;
        }
    }

    void SetCurrentCharacter(CharacterData character)
    {
        currentCharacter = character;

        if (character.skins == null || character.skins.Count == 0)
        {
            LoadBasePreview(character);
            ClearSkinButtons();
        }
        else
        {
            string savedSkin = characterDB.SelectedCharacterManager.GetSelectedSkin();
            SkinData skinToLoad = character.skins.Find(s => s.skinName == savedSkin);

            if (skinToLoad == null || !SkinUnlockSystem.IsSkinUnlocked(character.characterName, skinToLoad.skinName, skinToLoad.isUnlockedByDefault))
            {
                skinToLoad = character.skins.Find(s => SkinUnlockSystem.IsSkinUnlocked(character.characterName, s.skinName, s.isUnlockedByDefault));
            }

            if (skinToLoad != null)
                LoadPreview(character, skinToLoad);
            else
                LoadBasePreview(character);

            ShowSkinsForCharacter(character);
        }
    }

    void LoadBasePreview(CharacterData character)
    {
        if (currentCharacterPreview != null)
            Destroy(currentCharacterPreview);

        currentCharacter = character;
        currentSkin = null;
        characterName.text = currentCharacter.characterName;
        story.text = currentCharacter.characterStory;
        boardDetails.text = character.characterStory;

        currentCharacterPreview = Instantiate(character.characterPrefab, previewRoot.transform);
        currentCharacterPreview.transform.localPosition = Vector3.zero;
        currentCharacterPreview.transform.localScale = Vector3.zero;
        currentCharacterPreview.transform.DOScale(character.animateScale, 0.3f).SetEase(Ease.OutBack);

        characterDB.SelectedCharacterManager.Save(character.characterName, "");
    }

    void LoadPreview(CharacterData character, SkinData skin)
    {
        if (currentCharacterPreview != null)
            Destroy(currentCharacterPreview);

        bool skinUnlocked = SkinUnlockSystem.IsSkinUnlocked(character.characterName, skin.skinName, skin.isUnlockedByDefault);
        if (!skinUnlocked)
        {
            popupManager.Show("This skin is locked.");
            return;
        }

        currentCharacter = character;
        currentSkin = skin;
        currentCharacter.material.mainTexture = skin.skin;
        story.text = currentCharacter.characterStory;
        characterName.text = character.characterName;
        boardDetails.text = character.characterStory;
        currentCharacterPreview = Instantiate(character.characterPrefab, previewRoot.transform);
        currentCharacterPreview.transform.localPosition = Vector3.zero;
        currentCharacterPreview.transform.localScale = Vector3.zero;
        currentCharacterPreview.transform.DOScale(character.animateScale, 0.3f).SetEase(Ease.OutBack);

        characterDB.SelectedCharacterManager.Save(character.characterName, skin.skinName);
    }

    void ShowSkinsForCharacter(CharacterData character)
    {
        ClearSkinButtons();

        foreach (var skin in character.skins)
        {
            GameObject btn = Instantiate(skinButtonPrefab, skinButtonContainer);
            skinButtons.Add(btn);
            buttonSkinMap[btn] = skin;

            var icon = btn.transform.Find("Icon").GetComponent<Image>();
            icon.sprite = skin.skinIcon;

            bool unlocked = SkinUnlockSystem.IsSkinUnlocked(character.characterName, skin.skinName, skin.isUnlockedByDefault);

            btn.transform.Find("Lock").gameObject.SetActive(!unlocked);
            btn.transform.Find("Price").GetComponent<TMP_Text>().text = unlocked ? "" : $"{skin.price}";

            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                ResetSkinIcons();

                if (unlocked)
                {
                    LoadPreview(character, skin);
                    icon.sprite = skin.skinIconSelected;
                }
                else
                {
                    if (CurrencyManager.Spend(skin.price))
                    {
                        SkinUnlockSystem.UnlockSkin(character.characterName, skin.skinName);
                        popupManager.Show($"Unlocked skin: {skin.skinName}");
                        ShowSkinsForCharacter(character);
                        LoadPreview(character, skin);
                    }
                    else
                    {
                        popupManager.Show("Not enough coins for this skin.");
                    }
                }
            });

            // Mark saved skin selected initially
            string savedSkin = characterDB.SelectedCharacterManager.GetSelectedSkin();
            if (skin.skinName == savedSkin)
            {
                icon.sprite = skin.skinIconSelected;
            }
        }
    }

    void ResetSkinIcons()
    {
        foreach (var btn in skinButtons)
        {
            if (buttonSkinMap.TryGetValue(btn, out SkinData skin))
            {
                var icon = btn.transform.Find("Icon").GetComponent<Image>();
                icon.sprite = skin.skinIcon;
            }
        }
    }

    void ClearSkinButtons()
    {
        foreach (Transform child in skinButtonContainer)
            Destroy(child.gameObject);

        skinButtons.Clear();
        buttonSkinMap.Clear();
    }

    void LoadLastSelectedCharacter()
    {
        string savedCharacter = characterDB.SelectedCharacterManager.GetSelectedCharacter();
        string savedSkin = characterDB.SelectedCharacterManager.GetSelectedSkin();

        CharacterData character = characterDB.characters.Find(c => c.characterName == savedCharacter);

        if (character != null)
        {
            if (character.skins == null || character.skins.Count == 0)
            {
                LoadBasePreview(character);
                ClearSkinButtons();
            }
            else
            {
                SkinData skinToLoad = character.skins.Find(s => s.skinName == savedSkin);

                if (skinToLoad == null || !SkinUnlockSystem.IsSkinUnlocked(character.characterName, skinToLoad.skinName, skinToLoad.isUnlockedByDefault))
                {
                    skinToLoad = character.skins.Find(s => SkinUnlockSystem.IsSkinUnlocked(character.characterName, s.skinName, s.isUnlockedByDefault));
                }

                if (skinToLoad != null)
                    LoadPreview(character, skinToLoad);
                else
                    LoadBasePreview(character);

                ShowSkinsForCharacter(character);
            }
        }
        else
        {
            character = characterDB.characters[0];
            currentCharacter = character;
            LoadBasePreview(character);
            characterDB.SelectedCharacterManager.Save(character.characterName, "Board");
        }
    }
}
