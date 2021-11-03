using UnityEngine;

public abstract class APlayer : MonoBehaviour, IHealable, IDamagable
{
    private GameDifficult _gameDifficultInstance;
    protected GameSettingsSO currentDifficult;
    
    private float _maxHealth;
    private float _health;

    protected void Start()
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

    public void Heal(float value)
    {
        _health += value;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }
}
