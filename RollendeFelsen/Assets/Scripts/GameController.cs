using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [Header("Rock Settings")]
    [SerializeField] GameObject rockPrefab;
    [SerializeField] private Collider rockSpawner;
    [SerializeField] private float frecuency;

    List<Actor> actorsPos;
    public Object[] players; //Only for calculate how many players are in the current game
    int positions;
    [SerializeField] private Collider playerSpawner;

    [SerializeField] Text[] txtPositions;

    public delegate void Win();
    public event Win OnWin, OnLooser;

    private void Start()
    {
        WinCondition winCondition = (WinCondition)FindObjectOfType(typeof(WinCondition));
        winCondition.OnFinish += PlayersFinishing;

        players = FindObjectsOfType(typeof(Actor));
        actorsPos = new List<Actor>();

        for (int i = 0; i < players.Length; i++)
        {
            txtPositions[i].enabled = true;
        }

        InvokeRepeating("SpawnRock", 1f, frecuency);
    }

    private void SpawnRock()
    {
        Instantiate(rockPrefab, GetSpawnPoint(rockSpawner), Quaternion.identity);
    }

    private void PlayersFinishing(Actor _playerFinish)
    {
        actorsPos.Add(_playerFinish);
        txtPositions[positions].text = (positions + 1).ToString() + " " + actorsPos[positions].name + " Finish";
        positions++;

        if (actorsPos.Count == players.Length)
        {
            if (actorsPos[0] is Player)
            {
                print("Win");
                OnWin();
            }
            else {
                print("Looser");
                OnLooser();
            }
        }
    }

    private Vector3 GetSpawnPoint(Collider _spawnZone)
    {
        Vector3 spawnPoint = Vector3.zero;

        spawnPoint = new Vector3(Random.Range(_spawnZone.bounds.min.x, _spawnZone.bounds.max.x), Random.Range(_spawnZone.bounds.min.y, _spawnZone.bounds.max.y),
            Random.Range(_spawnZone.bounds.min.z, _spawnZone.bounds.max.z));

        return spawnPoint;
    }

    public IEnumerator SpawnPlayer(Actor _actor)
    {
        _actor.transform.position = GetSpawnPoint(playerSpawner);
        if(_actor.gameObject.GetComponent<Enemy>() != null)
        {
            _actor.gameObject.GetComponent<Enemy>().enabled = false;
            _actor.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        }
        _actor.enabled = false;
        _actor.gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(5);
        _actor.enabled = true;
        if (_actor.gameObject.GetComponent<Enemy>() != null)
        {
            _actor.gameObject.GetComponent<Enemy>().enabled = true;
            _actor.gameObject.GetComponent<NavMeshAgent>().speed = 3;
        }
        _actor.gameObject.GetComponent<Collider>().enabled = true;
    }
}
