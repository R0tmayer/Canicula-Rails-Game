using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour, IHealable, IDamagable
{
    private GameDifficult _gameDifficultInstance;
    private GameSettingsSO currentDifficult;
    
    private float _maxHealth;
    private float _health;
    
    public event Action Hit;

    private void Start()
    {
        _gameDifficultInstance = FindObjectOfType<GameDifficult>();
        currentDifficult = _gameDifficultInstance.CurrentDifficult;
        
        _maxHealth = currentDifficult.PlayerMaxHealth;
        _health = _maxHealth;
    }
    
    private void Die()
    {
        Debug.LogError("YOU DIED! PLAYER HP IS " + _health );
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        Hit?.Invoke();

        if (_health <= 0)
        {
            Die();
        }
    }

    public void TakeHeal(float healValue)
    {
        _health += healValue;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }
}
