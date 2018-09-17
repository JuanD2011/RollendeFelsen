using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public Transform[] playerSpawns;

    [Header("Rock Settings")]
    [SerializeField] GameObject rockPrefab;
    [SerializeField] private Transform[] rockSpawns;
    [SerializeField] private float frecuency;

    List<Actor> actorsPos;
    public Object[] players; //Only to calculate how many players are in the current game
    int positions;

    [SerializeField] Text[] txtPositions;

    public delegate void Win();
    public event Win OnWin, OnLooser;

    private void Start()
    {
        WinCondition winCondition = (WinCondition)FindObjectOfType(typeof(WinCondition));
        winCondition.OnFinish += PlayersFinishing;

        players = FindObjectsOfType(typeof(Actor));
        actorsPos = new List<Actor>();

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

    private void PlayersFinishing(Actor _playerFinish) {
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
}
