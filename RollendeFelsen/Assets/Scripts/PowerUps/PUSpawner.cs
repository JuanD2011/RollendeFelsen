using UnityEngine;

public class PUSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] powerUpsPrefabs;

    [SerializeField] private float spawnTime;
    private GameObject randomPU;
    [SerializeField] private Collider boxCollider;
    LayerMask layer;

    private void Start()
    {
        InvokeRepeating("SpawnRandomPU", 0, spawnTime);
        layer = 9;
    }

    private void SpawnRandomPU()
    {
        spawnTime = Random.Range(5, 10);
        randomPU = powerUpsPrefabs[Random.Range(0, powerUpsPrefabs.Length)];

        Vector3 position = GetSpawnPoint();
        Instantiate(randomPU, position, Quaternion.identity);
    }


    private Vector3 GetSpawnPoint()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x), Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y),
                Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z));
        
        while (!Physics.Raycast(spawnPoint, spawnPoint - boxCollider.bounds.center, 10f, layer))
        {
            spawnPoint = new Vector3(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x), Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y), 
                Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z));
        }
        return spawnPoint;
    }
}
