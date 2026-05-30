using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterSkin", menuName = "Character Selection/Character Skin")]
public class SkinData : ScriptableObject
{
    public string skinName;
    public Sprite skinIcon;
    public Sprite skinIconSelected;
    public Texture skin;
    public int price;
    public bool isUnlockedByDefault;
}
