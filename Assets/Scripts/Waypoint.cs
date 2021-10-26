﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Waypoint : MonoBehaviour
{
    private SpawnPoint[] _spawnPoints;
    private List<Enemy> _enemies;
    private DataSceneStorage _dataSceneStorage;

    public event Action AllEnemiesDied;

    private void Awake()
    {
        _dataSceneStorage = FindObjectOfType<DataSceneStorage>();
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        _enemies = new List<Enemy>();
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void OnEnemyDied(Enemy enemy)
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
                _spawnPoints[i].transform.position,
                Quaternion.identity,
                _spawnPoints[i].transform.parent);

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
