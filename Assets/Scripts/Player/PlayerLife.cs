using System;
using UnityEngine;

public class PlayerLife : MonoBehaviour, IHealable, IDamagable
{
    private GameDifficult _gameDifficult;
    private GameSettingsSO currentDifficult;
    
    private float _maxHealth;
    private float _health;
    
    public event Action<float> HealthChanged;
    public event Action Died;
    public event Action Hitted;

    private void Start()
    {
        _gameDifficult = FindObjectOfType<GameDifficult>();
        currentDifficult = _gameDifficult.CurrentDifficult;
        
        _maxHealth = currentDifficult.PlayerMaxHealth;
        _health = _maxHealth;
        
        HealthChanged?.Invoke(_health);
    }
    
    public void TakeDamage(float damage)
    {
        _health -= damage;
        Hitted?.Invoke();
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
        {
            Died?.Invoke();
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
