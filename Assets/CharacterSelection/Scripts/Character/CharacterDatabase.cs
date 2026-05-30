using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Character Selection/Character Database")]
public class CharacterDatabase : ScriptableObject
{
    public List<CharacterData> characters;

    private SelectedCharacterManager selectedCharacterManager;



    [Button]
    void SetAllIndexes()
    {
        for (int i = 1; i < characters.Count; i++)
        {
            characters[i].unlockRequriment = i + 1;
        }
    }

    public SelectedCharacterManager SelectedCharacterManager 
    { 
        get
        {
            if (selectedCharacterManager == null)
                selectedCharacterManager = new SelectedCharacterManager(name, name);

            return selectedCharacterManager;
        }
    }
}
