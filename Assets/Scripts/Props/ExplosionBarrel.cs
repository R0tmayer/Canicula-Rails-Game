using UnityEngine;

public class ExplosionBarrel : MonoBehaviour, IDamagable
{
    private GameDifficult _gameDifficultInstance;
    private GameSettingsSO _currentDifficult;
    
    private float _health;
    private float _explosionRadius;
    private float _damage;

    private void Start()
    {
        _gameDifficultInstance = FindObjectOfType<GameDifficult>();
        _currentDifficult = _gameDifficultInstance.CurrentDifficult;
        
        _health = _currentDifficult.BarrelMaxHealth;
        _explosionRadius = _currentDifficult.BarrelRadius;
        _damage = _currentDifficult.BarrelDamage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
    
    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out AEnemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }
        
        gameObject.SetActive(false);
    }


    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Explode();
        }
    }
}
