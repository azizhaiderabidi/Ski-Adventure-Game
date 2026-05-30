using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ModelLoadler : MonoBehaviour
{
    [SerializeField] CharacterDatabase characterDB;
    [SerializeField] GameObject[] characters;

    private void Start()
    {
       // SetPlayer();
    }

    private void OnEnable()
    {
        //if(LevelTracker.Current)
        //{
        //    LoadModel(LevelTracker.Current.levelIndex);
        //}else
        //{
        //    SetPlayer();
        //}
        
    }

    public void SetPlayer()
    {
        int index = PlayerPrefs.GetInt(characterDB.name + "SelectedCharacter");

        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        characters[index].SetActive(true);
    }

    public void LoadModel(int index)
    {
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        characters[index].SetActive(true);
        Debug.Log("character Index:" + index);
    }
}
