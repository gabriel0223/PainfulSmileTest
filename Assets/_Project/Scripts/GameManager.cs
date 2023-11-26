using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnScoreUpdate;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _enemyPrefabs;

    private bool _isGameOver;
    private int _score;

    public float Timer { get; private set; }

    private void Awake()
    {
        Timer = 180;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private void Update()
    {
        if (_isGameOver)
        {
            return;
        }

        Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            EndGame();
        }
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (!_isGameOver)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(5f);
        }
    }

    private void SpawnEnemy()
    {
        GameObject randomEnemy = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
        Vector3 randomSpawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;

        GameObject spawnedEnemy = Instantiate(randomEnemy, randomSpawnPosition, Quaternion.identity);

        spawnedEnemy.GetComponent<ShipHealth>().OnDie += IncreasePlayerScore;
    }

    private void IncreasePlayerScore()
    {
        _score++;
        
        OnScoreUpdate?.Invoke(_score);
    }

    private void EndGame()
    {
        _isGameOver = true;
        Time.timeScale = 0;
    }
}
