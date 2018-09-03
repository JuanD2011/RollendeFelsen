using UnityEngine;

public class GameController : MonoBehaviour {
    [Header("Rock Settings")]
    [SerializeField] GameObject rockPrefab;
    [SerializeField] private Transform[] rockSpawns;
    [SerializeField] private float frecuency;

    private void Start()
    {
        InvokeRepeating("SpawnRock", 3f, frecuency);
    }

    private void SpawnRock()
    {
        int randomSpawn = Random.Range(0, rockSpawns.Length);
        Instantiate(rockPrefab, rockSpawns[randomSpawn].position, Quaternion.identity);
    }
}
