using UnityEngine;

public class ExplosionBarrel : MonoBehaviour, IDamagable
{
    private GameDifficult _gameDifficultInstance;
    private GameSettingsSO _currentDifficult;
    [SerializeField] private FirstAidKit _firstAidKitPrefab;
    
    private float _health;
    private float _explosionRadius;
    private float _damage;
    private HitSpawner _explosionEffect;

    public float Health => _health;

    private void Start()
    {
        _gameDifficultInstance = FindObjectOfType<GameDifficult>();
        _currentDifficult = _gameDifficultInstance.CurrentDifficult;
        
        _health = _currentDifficult.BarrelMaxHealth;
        _explosionRadius = _currentDifficult.BarrelRadius;
        _damage = _currentDifficult.BarrelDamage;
        _explosionEffect = _currentDifficult.BarrelExplosion;
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
        
        HitSpawner spawned = Instantiate(_explosionEffect, transform.position + Vector3.down, Quaternion.identity);
        
        gameObject.SetActive(false);
    }


    public void TakeDamage(float damage)
    {
        _health = Health - damage;

        if (Health <= 0)
        {
            Explode();
            Instantiate(_firstAidKitPrefab, transform.position + Vector3.up * 1.5f, _firstAidKitPrefab.transform.rotation);
        }
    }
}
