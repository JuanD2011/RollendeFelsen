using UnityEngine;

public class PUSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] powerUpsPrefabs;

    [SerializeField] Vector3 size;

    private float randomTime;
    private GameObject randomPU;

    private void Start()
    {
        Invoke("SpawnRandomPU", 3);
    }

    private void SpawnRandomPU()
    {
        randomTime = Random.Range(5, 10);
        randomPU = powerUpsPrefabs[Random.Range(0, powerUpsPrefabs.Length)];

        Vector3 position = transform.localPosition + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(randomPU, position, Quaternion.identity);

        Invoke("SpawnRandomPU", randomTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.localPosition, size);
    }
}
