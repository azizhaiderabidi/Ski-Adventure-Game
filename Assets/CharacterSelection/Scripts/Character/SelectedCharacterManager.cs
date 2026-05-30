using UnityEngine;

public class SelectedCharacterManager
{
    private const string CharacterKey = "SELECTED_CHARACTER";
    private const string SkinKey = "SELECTED_SKIN";
    
    private string _characterKey = "SELECTED_CHARACTER";
    private string _skinKey = "SELECTED_SKIN";


    public SelectedCharacterManager(string CharacterKey, string SkinKey)
    {
        _characterKey = SelectedCharacterManager.CharacterKey;
        _skinKey = SelectedCharacterManager.SkinKey;

        _characterKey += CharacterKey;
        _skinKey += SkinKey;
    }

    public void Save(string characterName, string skinName)
    {
        PlayerPrefs.SetString(_characterKey, characterName);
        PlayerPrefs.SetString(_skinKey, skinName);
        PlayerPrefs.Save();
    }
    public string GetSelectedCharacter() => PlayerPrefs.GetString(_characterKey, "");
    public string GetSelectedSkin() => PlayerPrefs.GetString(_skinKey, "");
}
