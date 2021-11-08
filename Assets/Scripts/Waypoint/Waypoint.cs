using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Waypoint : MonoBehaviour
{
    private SpawnPoint[] _spawnPoints;
    private List<AEnemy> _enemies;
    private DataSceneStorage _dataSceneStorage;

    public event Action AllEnemiesDied;

    public float EnemiesCount => _enemies.Count;

    private void Awake()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        _enemies = new List<AEnemy>();
        _dataSceneStorage = FindObjectOfType<DataSceneStorage>();
    }

    private void Start()
    {
        SpawnEnemies();

    }

    private void OnEnemyDied(AEnemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        _enemies.Remove(enemy);

        if (_enemies.Count == 0)
        {
            AllEnemiesDied?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void SpawnEnemies()
    {
        var enemiesAmount = _spawnPoints.Length;

        for (int i = 0; i < enemiesAmount; i++)
        {
            var randomEnemyIndex = Random.Range(0, _dataSceneStorage.Pull.Length);
            var spawned = Instantiate(_dataSceneStorage.Pull[randomEnemyIndex],
                _spawnPoints[i].transform.position + Vector3.down,
                Quaternion.identity);

            spawned.Died += OnEnemyDied;
            spawned.gameObject.SetActive(false);
            _enemies.Add(spawned);
        }
    }

    public void ActivateEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
    
}
