using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessEnvironment : MonoBehaviour
{
    public GameObject[] snowenvironmentPrefabs; // Snow environment prefabs
    public GameObject[] DesertenvironmentPrefabs; // Desert environment prefabs
    public Transform player; // Player reference
    public float environmentLength = 10f; // Length of each environment prefab
    private Queue<GameObject> activeEnvironments = new Queue<GameObject>(); // Track active environments
    private float nextSpawnZ = 0f; // Next Z position to spawn environment
    public float playerDistance;
    public int currentSelectedMap;

    void Start()
    {
        currentSelectedMap = PlayerPrefs.GetInt("SelectedMap", 0); // Load selected map
        UpdateMap(); // Ensure correct environment is used
    }

    void Update()
    {
        if (player.position.z - playerDistance > nextSpawnZ - (2 * environmentLength))
        {
            SpawnEnvironment();
            RemoveOldEnvironment();
        }
    }

    void SpawnEnvironment()
    {
        GameObject[] selectedPrefabs = currentSelectedMap == 0 ? snowenvironmentPrefabs : DesertenvironmentPrefabs;

        if (selectedPrefabs.Length == 0) return; // Safety check

        GameObject env = Instantiate(selectedPrefabs[Random.Range(0, selectedPrefabs.Length)]);
        env.transform.position = new Vector3(0, 0, nextSpawnZ);
        nextSpawnZ += environmentLength;
        activeEnvironments.Enqueue(env);
    }

    void RemoveOldEnvironment()
    {
        if (activeEnvironments.Count > 2)
        {
            GameObject oldEnv = activeEnvironments.Dequeue();
            Destroy(oldEnv);
        }
    }

    public void UpdateMap()
    {
        currentSelectedMap = PlayerPrefs.GetInt("SelectedMap", 0); // Load selected map
        nextSpawnZ = 0f; // Reset spawn position

        // Purane environments hatao
        while (activeEnvironments.Count > 0)
        {
            Destroy(activeEnvironments.Dequeue());
        }

        // Naya environment spawn karo
        for (int i = 0; i < 2; i++)
        {
            SpawnEnvironment();
        }
    }
}
