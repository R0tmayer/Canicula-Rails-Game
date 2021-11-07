using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class AEnemy : MonoBehaviour, IDamagable
{
     private GameDifficult _gameDifficultInstance;
     protected GameSettingsSO currentDifficult;

     protected float startDelay;
     protected float health;
     protected float moveSpeed;
     
     protected PlayerLife player;
     protected Animator animator;

     public event Action<float, float> HealthChanged;
     public event Action<AEnemy> Died;

     private float _botsTimeDeactivate;
     private const string _runTrigger = "Run";
     private const string _dieTrigger = "Die";

     private ParticleSystem _spawnEffect;
     private ParticleSystem _dieEffect;

     private void Awake()
     {
          _gameDifficultInstance = FindObjectOfType<GameDifficult>();
          currentDifficult = _gameDifficultInstance.CurrentDifficult;

          player = FindObjectOfType<PlayerLife>();
          animator = GetComponent<Animator>();
          
          startDelay = currentDifficult.BotsStartDelay;
          health = currentDifficult.BotsMaxHealth;
          _botsTimeDeactivate = currentDifficult.BotsTimeDeactivate;
     }

     private void OnEnable()
     {
          Instantiate(currentDifficult.SpawnBotEffect, transform);
     }

     private void Die()
     {
          DieAnimation();
          Died?.Invoke(this);
          GetComponent<Collider>().enabled = false;
          Instantiate(currentDifficult.DieBotEffect, transform.position + Vector3.up * 2.5f, Quaternion.identity);
          moveSpeed = 0;
          
          StartCoroutine(Deactivate());
     }

     private IEnumerator Deactivate()
     {
          yield return new WaitForSeconds(_botsTimeDeactivate);
          gameObject.SetActive(false);
     }

     private void DieAnimation()
     {
          animator.SetTrigger(_dieTrigger);
     }

     protected void RunAnimation()
     {
          animator.SetTrigger(_runTrigger);
     }

     public void TakeDamage(float damage)
     {
          health -= damage;
          HealthChanged?.Invoke(health, currentDifficult.BotsMaxHealth);

          if (health <= 0)
          {
               Die();
          }
     }
}
