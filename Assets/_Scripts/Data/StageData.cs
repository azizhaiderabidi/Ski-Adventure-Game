
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject {
    public int xpRequired;
    public string stageName;
   // public MiniGameType miniGameType;
    public Sprite icon;
    public string rewardName;
}


