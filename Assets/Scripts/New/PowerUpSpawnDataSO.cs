using UnityEngine;

[CreateAssetMenu(menuName = "Game/PowerUpDataSO")]
public class PowerUpSpawnDataSO : ScriptableObject
{
    public PowerUpType powerUpType;
    public float spawnChance = 0.5f;
    public ParticleEffect collectVFX;
    public AudioClip collectSound;
}

public enum PowerUpType
{
    Coins2x , Magnet , SpeedBoost , None
}