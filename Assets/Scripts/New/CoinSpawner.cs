using UnityEngine;
using DG.Tweening;

public class CoinSpawner : IItemSpawner
{
    private ObjectPooler pooler;
    private CoinSpawnDataSO data;

    public CoinSpawner(ObjectPooler pooler, CoinSpawnDataSO data)
    {
        this.pooler = pooler;
        this.data = data;
    }

    public void Spawn(Transform[] points)
    {
        foreach (var point in points)
        {
            //if (Random.value <= data.spawnChance)
            {
                GameObject coin = pooler.SpawnFromPool(data.prefabName, point.position, Quaternion.identity);
                //Debug.Log(coin);
                coin.transform.DOScale(1.2f, 0.3f).SetLoops(2, LoopType.Yoyo);

                if (data.spawnVFX != null)
                    Object.Instantiate(data.spawnVFX, point.position, Quaternion.identity);

                var coinScript = coin.GetComponent<RuntimeCoin>();
                if (coinScript != null) coinScript.Setup(data);
            }
            //Debug.Log("SpawnCoin");
        }
    }
}