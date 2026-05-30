using UnityEngine;

[CreateAssetMenu(menuName = "Game/CoinSpawnData")]
public class CoinSpawnDataSO : ScriptableObject
{
    public string prefabName;
    public float spawnChance = 0.7f;
    public GameObject spawnVFX;
    public ParticleEffect collectVFX;
    public AudioClip collectSound;
    public int coinValue = 1;
}