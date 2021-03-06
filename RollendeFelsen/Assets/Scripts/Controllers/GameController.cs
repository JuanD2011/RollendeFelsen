﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    #region Singleton
    public static GameController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else {
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    [Header("Rock Settings")]
    [SerializeField] GameObject rockPrefab;
    [SerializeField] private Collider rockSpawner;
    [SerializeField] private float frecuency;

    private List<Actor> actorsPos;
    private List<Actor> players; //Only for calculate how many players are in the current game
    int positions;
    [SerializeField] private Collider playerSpawner;

    [SerializeField] Text[] txtPositions;

    public delegate void FinishGame(bool _finishCondition);
    public FinishGame delWin, delLoose;

    public List<Actor> Players
    {
        get
        {
            return players;
        }

        set
        {
            players = value;
        }
    }

    private void Start()
    {
        WinCondition winCondition = (WinCondition)FindObjectOfType(typeof(WinCondition));
        winCondition.OnFinish += PlayersFinishing;

        Players = new List<Actor>();
        actorsPos = new List<Actor>();
        Players.AddRange(FindObjectsOfType<Actor>());

        for (int i = 0; i < Players.Count; i++)
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
        print(actorsPos.Count);

        if (actorsPos.Count >= Players.Count)
        {
            if (actorsPos[0] is Player)
            {
                delWin(true);
                print("Win");
            }
            else {
                delLoose(false);
                print("Looser");
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
        if (_actor is Enemy)
        {
            Enemy enemy = _actor as Enemy;
            enemy.Agent.isStopped = true;
        }
        else if (_actor is Player) {
            _actor.enabled = false;
        }
        _actor.Immunity = true;
        yield return new WaitForSeconds(5f);
        _actor.Immunity = false;
        if (_actor is Enemy)
        {
            Enemy enemy = _actor as Enemy;
            enemy.Agent.isStopped = false;
        }
        else if (_actor is Player) {
            _actor.enabled = true;
        }
    }
}
