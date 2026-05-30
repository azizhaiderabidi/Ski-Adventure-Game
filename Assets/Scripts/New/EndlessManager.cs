using System.Collections.Generic;
using UnityEngine;

public class EndlessManager : MonoBehaviour
{
    public Transform player;
    public float environmentLength = 50f;
    public float playerTriggerDistance = 100f;

    private float nextSpawnZ = 0f;
    private Queue<GameObject> activeSegments = new();
    public int maxSegments = 3;

    public GameObject[] environmentPrefabs;
    public ObjectPooler pooler;

    void Start()
    {
        SpawnInitialSegments();
    }

    void Update()
    {
        if (player.position.z + playerTriggerDistance > nextSpawnZ)
        {
            SpawnSegment();
            RemoveOldestSegment();
        }
    }

    void SpawnInitialSegments()
    {
        for (int i = 0; i < maxSegments; i++) SpawnSegment();
    }

    void SpawnSegment()
    {
        GameObject prefab = environmentPrefabs[Random.Range(0, environmentPrefabs.Length)];
        GameObject segment = Instantiate(prefab, new Vector3(0, 0, nextSpawnZ), Quaternion.identity);
       // segment.GetComponent<TrackSegment>()?.Setup(pooler);
        nextSpawnZ += environmentLength;
        activeSegments.Enqueue(segment);
    }

    void RemoveOldestSegment()
    {
        if (activeSegments.Count > maxSegments)
        {
            GameObject oldest = activeSegments.Dequeue();
            Destroy(oldest);
        }
    }
}
