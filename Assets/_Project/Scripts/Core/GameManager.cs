using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnScoreUpdate;
    public event Action OnGameOver;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private ShipHealth _playerHealth;

    private bool _isGameOver;
    private float _timeBetweenEnemySpawns;

    public float Timer { get; private set; } 
    public int Score { get; private set; }

    private void Awake()
    {
        Timer = GameSettings.GameSessionTime * 60;
        _timeBetweenEnemySpawns = GameSettings.EnemySpawnTime;
    }

    private void OnEnable()
    {
        _playerHealth.OnDie += EndGame;
    }

    private void OnDisable()
    {
        _playerHealth.OnDie -= EndGame;
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (!_isGameOver)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(_timeBetweenEnemySpawns);
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
        if (_isGameOver)
        {
            return;
        }

        Score++;
        
        OnScoreUpdate?.Invoke(Score);
    }

    private void EndGame()
    {
        _isGameOver = true;
        _playerHealth.GetComponent<InputManager>().DisableInput();

        OnGameOver?.Invoke();
    }
}
