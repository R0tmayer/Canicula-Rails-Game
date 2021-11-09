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
    
    public event Action<float> HealthChanged;
    public event Action Died;
    public event Action Hitted;

    private void Start()
    {
        _gameDifficultInstance = FindObjectOfType<GameDifficult>();
        currentDifficult = _gameDifficultInstance.CurrentDifficult;
        
        _maxHealth = currentDifficult.PlayerMaxHealth;
        _health = _maxHealth;
        
        HealthChanged?.Invoke(_health);
    }
    
    private void Die()
    {
        // Debug.LogWarning("YOU DIED! PLAYER HP IS " + _health );
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        HealthChanged?.Invoke(_health);
        Hitted?.Invoke();

        if (_health <= 0)
        {
            Died?.Invoke();
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

        HealthChanged?.Invoke(_health);
    }
}
