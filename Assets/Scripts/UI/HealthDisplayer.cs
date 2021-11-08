﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayer : MonoBehaviour
{
    [SerializeField] private Text _healthText;
    private PlayerLife _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerLife>();
        _player.HealthChanged += OnHealthChanged;

    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }



    private void OnHealthChanged(float health)
    {
        _healthText.text = health.ToString();
    }
}