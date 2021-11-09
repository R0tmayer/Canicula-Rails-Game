using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    private AEnemy _enemy;
    private float _maxHealth;

    private void Awake()
    {
        _enemy = GetComponentInParent<AEnemy>();
    }

    private void OnEnable()
    {
        _enemy.HealthChanged += OnHealthChanged;
    }

    private void Update()
    {
        _maxHealth = _enemy.MaxHealth;
    }


    private void OnDisable()
    {
        _enemy.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float currentHealth)
    {
        _bar.fillAmount = currentHealth / _maxHealth;
    }

}
