using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PUSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] powerUpsPrefabs;

    private float randomTime;
    private GameObject randomPU;
    private Collider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<Collider>();
        Invoke("SpawnRandomPU", 3);
    }

    private void SpawnRandomPU()
    {
        randomTime = Random.Range(5, 10);
        randomPU = powerUpsPrefabs[Random.Range(0, powerUpsPrefabs.Length)];

        Vector3 position = GetSpawnPoint();
        Instantiate(randomPU, position, Quaternion.identity);

        Invoke("SpawnRandomPU", randomTime);
    }


    private Vector3 GetSpawnPoint()
    {
        RaycastHit hit;
        Vector3 spawnPoint = new Vector3(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x), Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y),
                Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z));
        
        while (!Physics.Raycast(spawnPoint, spawnPoint - boxCollider.bounds.center, out hit, Mathf.Infinity, 9))
        {
            Debug.DrawRay(spawnPoint, boxCollider.bounds.center, Color.yellow);
            spawnPoint = new Vector3(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x), Random.Range(boxCollider.bounds.min.y, boxCollider.bounds.max.y), 
                Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z));
        }
        return spawnPoint;
    }
}
