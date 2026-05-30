

using Sirenix.OdinInspector;
using UnityEngine;



public class TrackSegment : MonoBehaviour
{
    public Transform[] coinSpawnPoints;
    public Transform[] obstacleSpawnPoints;
    public Transform[] powerUpSpawnPoints;

    public CoinSpawnDataSO coinData;
    public ObstacleSpawnDataSO obstacleData;
    public PowerUpSpawnDataSO[] powerdataData;


    private void Start()
    {
       Init();
    }
    public void Setup(ObjectPooler pooler)
    {
        var coinSpawner = new CoinSpawner(pooler, coinData);
        var obstacleSpawner = new ObstacleSpawner(pooler, obstacleData);
        var powerUpSpawner = new PowerUpSpawner(pooler, powerdataData[Random.Range(0, powerdataData.Length)]);

        coinSpawner.Spawn(coinSpawnPoints);
        obstacleSpawner.Spawn(obstacleSpawnPoints);
        powerUpSpawner.Spawn(powerUpSpawnPoints);
    }

    [Button]
    void Init()
    {
        Setup(ObjectPooler.Instance);
    }
}


