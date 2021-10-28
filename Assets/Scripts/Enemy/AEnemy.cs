using System;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour, IDamagable
{
     [SerializeField] protected int health;

     [SerializeField] protected float moveSpeed;
     protected APlayer player;
     
     public event Action<AEnemy> Died;
     
     private void Awake()
     {
          player = FindObjectOfType<APlayer>();
     }
     
     public void TakeDamage(int damage)
     {
          health -= damage;

          if (health <= 0)
          {
               Die();
          }
     }

     private void Die()
     {
          Died?.Invoke(this);
          gameObject.SetActive(false);
     }


}
