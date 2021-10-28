using UnityEngine;

public abstract class APlayer : MonoBehaviour, IHealable, IDamagable
{
    [SerializeField] private int _maxHealth;
    private int _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void Heal(int value)
    {
        _health += value;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.LogError("YOU DIED! PLAYER HP IS " + _health );
    }
}
