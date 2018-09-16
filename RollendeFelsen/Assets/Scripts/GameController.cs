using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Transform[] playerSpawns;

    [Header("Rock Settings")]
    [SerializeField] GameObject rockPrefab;
    [SerializeField] private Transform[] rockSpawns;
    [SerializeField] private float frecuency;

    int positions;
    Actor[] actorsPos;
    public Object[] players; //Only to calculate how many players are in the current game

    [SerializeField] Text[] txtPositions;

    private void Start()
    {
        WinCondition winCondition = (WinCondition)FindObjectOfType(typeof(WinCondition));
        winCondition.OnFinish += PlayersFinishing;
        Actor.OnDeath += DeadPlayers;

        players = FindObjectsOfType(typeof(Actor));
        positions = players.Length;
        actorsPos = new Actor[positions];

        for (int i = 0; i < players.Length; i++) {
            txtPositions[i].enabled = true;
        }

        InvokeRepeating("SpawnRock", 3f, frecuency);

    }

    private void SpawnRock()
    {
        int randomSpawn = Random.Range(0, rockSpawns.Length);
        Instantiate(rockPrefab, rockSpawns[randomSpawn].position, Quaternion.identity);
    }

    private void DeadPlayers(Actor _deadActor) {
        for (int i = actorsPos.Length - 1; i > 0; i--) {
            if (actorsPos[i] == null) {
                actorsPos[i] = _deadActor;
                txtPositions[i].text = (i + 1).ToString() + " " + actorsPos[i].name + " Dead";
                break;
            }
        }
    }

    private void PlayersFinishing(Actor _playerFinish) {
        for (int i = 0; i < actorsPos.Length; i++) {
            if (actorsPos[i] == null) {
                actorsPos[i] = _playerFinish;
                txtPositions[i].text = (i + 1).ToString() + " " + actorsPos[i].name + " Finish";
                break;
            }
        }

    }
}
