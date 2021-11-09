using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private PlayerLife _player;
    private DataSceneStorage _data;    
    private GameDifficult _gameDifficultInstance;

    private float _currentHealth;
    private float _maxHealth;

    private float _totalHits;
    private float _totalShots;

    private Sprite _reachedMedal;

    public float FinalScores { get; private set; }
    public float HealthPercent { get; private set; }
    public float AccuracyPercent { get; private set; }
    public float CompletionPercent { get; private set; }

    public Sprite ReachedMedal => _reachedMedal;

    private void Start()
    {
        _player = FindObjectOfType<PlayerLife>();
        _gameDifficultInstance = FindObjectOfType<GameDifficult>();
        _data = FindObjectOfType<DataSceneStorage>();

        _player.HealthChanged += OnPlayerHealthChanged;
        _maxHealth = _gameDifficultInstance.CurrentDifficult.PlayerMaxHealth;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _totalShots++;
        }
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnPlayerHealthChanged;
    }

    public void CalculateScores()
    {
        var healthScores = (_currentHealth / _maxHealth) * _data.HealthBonus;
        var accuracyScores = _totalShots == 0 ? 0 : (_totalHits / _totalShots) * _data.AccuracyBonus;
        var completionScores = (_data.CurrentWaypointIndex / _data.WaypointsCount) * _data.CompletionBonus;

        FinalScores = healthScores + accuracyScores + completionScores;

        
    }
    
    public void CalculateStatsPercents()
    {
        HealthPercent = (_currentHealth / _maxHealth) * 100f;
        AccuracyPercent = _totalShots == 0 ? 0 : (_totalHits / _totalShots) * 100f;
        CompletionPercent = (_data.CurrentWaypointIndex / _data.WaypointsCount) * 100f;
    }

    public void CalculateReachedMedal()
    {
        if (FinalScores < _data.GoldGoal)
        {
            _reachedMedal = _data.SilverMedal;

            if (FinalScores < _data.SilverGoal)
            {
                _reachedMedal = _data.BronzeMedal;

                if (FinalScores < _data.BronzeGoal)
                {
                    _reachedMedal = _data.NoMedal;
                }
            }
        }
        else
        {
            _reachedMedal = _data.GoldMedal;
        }
    }

    public void IncreaseTotalHits()
    {
        _totalHits++;
    }

    private void OnPlayerHealthChanged(float health)
    {
        _currentHealth = health;
    }
}