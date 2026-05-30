using UnityEngine;


[CreateAssetMenu(menuName = "UI/UIScreen")]
public class UIScreenSO : ScriptableObject
{
    [Header("Screen Info")]
    public string screenID;

    [Header("Prefab Reference")]
    public GameObject screenPrefab;
}
