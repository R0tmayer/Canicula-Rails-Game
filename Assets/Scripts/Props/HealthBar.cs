using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    private AEnemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<AEnemy>();
    }

    private void OnEnable()
    {
        _enemy.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _enemy.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        _bar.fillAmount = currentHealth / maxHealth;
    }

}
