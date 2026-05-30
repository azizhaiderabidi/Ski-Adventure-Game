using UnityEngine;
using DG.Tweening;

public class ObstacleSpawner : IItemSpawner
{
    private ObjectPooler pooler;
    private ObstacleSpawnDataSO data;

    public ObstacleSpawner(ObjectPooler pooler, ObstacleSpawnDataSO data)
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
                GameObject obstacle = pooler.SpawnFromPool(data.prefabName, point.position, Quaternion.identity);
                obstacle.transform.DOPunchScale(Vector3.one * 0.3f, 0.4f, 4, 0.8f);
                //obstacle.transform.rotation = Random.rotation;

                var obstacleScript = obstacle.GetComponent<RuntimeObstacle>();
                if (obstacleScript != null) obstacleScript.Setup(data);
            }
        }
    }
}
