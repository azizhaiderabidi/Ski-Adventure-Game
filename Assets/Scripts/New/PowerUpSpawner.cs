using UnityEngine;
using DG.Tweening;

public class PowerUpSpawner : IItemSpawner
{
    private ObjectPooler pooler;
    private PowerUpSpawnDataSO data;

    public PowerUpSpawner(ObjectPooler pooler, PowerUpSpawnDataSO data)
    {
        this.pooler = pooler;
        this.data = data;
    }

    public void Spawn(Transform[] points)
    {
        foreach (var point in points)
        {
            if (Random.value <= data.spawnChance)
            {
                GameObject obstacle = pooler.SpawnFromPool(data.powerUpType.ToString(), point.position, Quaternion.identity);
                obstacle.transform.DOPunchScale(Vector3.one * 0.3f, 0.4f, 4, 0.8f);

                var obstacleScript = obstacle.GetComponent<RuntimePowerUp>();
                if (obstacleScript != null) obstacleScript.Setup(data);
            }
        }
    }
}