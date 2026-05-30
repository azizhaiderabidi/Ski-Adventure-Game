using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections; // Coroutine ke liye zaroori hai

public class RandomSphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab; // Sphere ka prefab
    public Transform spawnPoint;    // Spawn position
    public int minSize = 1;         // Minimum sphere size
    public int maxSize = 5;         // Maximum sphere size
    public float forceStrength = 10f; // X-axis force ka strength
    public int spawnCount = 10;     // Kitne spheres ek sath spawn hon
    public float spawnDelay = 0.5f; // Har sphere ke darmiyan kitna delay ho

    [Button]
    void SpawnMultipleSpheres()
    {
        StartCoroutine(SpawnWithDelay());
    }

    IEnumerator SpawnWithDelay()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnSphere();
            yield return new WaitForSeconds(spawnDelay); // Har sphere ke darmiyan delay
        }
    }

    void SpawnSphere()
    {
        // Random position (thoda variation add karne ke liye)
        Vector3 randomOffset = new Vector3(0, 0, Random.Range(0, 2f));
        Vector3 spawnPos = spawnPoint.position + randomOffset;

        // Sphere ka spawn
        GameObject newSphere = Instantiate(spherePrefab, spawnPos, Quaternion.identity);

        // Random size set karna
        float randomSize = Random.Range(minSize, maxSize);
        newSphere.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

        // Rigidbody check aur force add karna
        Rigidbody rb = newSphere.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = newSphere.AddComponent<Rigidbody>(); // Agar Rigidbody nahi hai to add karo
        }

        // X-axis par force apply karna
        rb.AddForce(Vector3.forward * forceStrength, ForceMode.Impulse);
        Destroy(newSphere, 5f);
    }
}
