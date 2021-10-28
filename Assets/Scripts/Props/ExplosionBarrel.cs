using UnityEngine;

public class ExplosionBarrel : MonoBehaviour, IDamagable
{
    [SerializeField] private int _explosionRadius;
    [SerializeField] private int _health;

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
                enemy.TakeDamage(100);
            }
        }
        
        gameObject.SetActive(false);
    }


    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Explode();
        }
    }
}
