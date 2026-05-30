using UnityEngine;

[CreateAssetMenu(menuName = "Game/ObstacleSpawnData")]
public class ObstacleSpawnDataSO : ScriptableObject
{
    public string prefabName;
    public float spawnChance = 0.5f;
    public ParticleEffect collectVFX;
    public AudioClip collectSound;
}
