using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character Selection/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;

    [Space]
    [TextArea(10,50)]
    public string characterStory;
    [Space]
    public Sprite characterIcon;
    [Space]
    public bool isInstantiated = false;
    public GameObject characterPrefab;
    public bool isUnlockedByDefault;

    public int unlockRequriment;
    public int price;
    public bool isSpecial;

    public List<SkinData> skins;
    public Material material;
    public Vector3 animateScale;

}
