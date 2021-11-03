using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
 
    private GameDifficult _gameDifficultInstance;
    private GameSettingsSO _currentDifficult;
    
    private Rigidbody _rigidbody;
    private APlayer _player;
    private float _timer;
    private const float _timeToDeactivate = 3;

    private void Awake()
    {
        _gameDifficultInstance = FindObjectOfType<GameDifficult>();
        _currentDifficult = _gameDifficultInstance.CurrentDifficult;
        
        _rigidbody = GetComponent<Rigidbody>();
        _player = FindObjectOfType<APlayer>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeToDeactivate)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = (_player.transform.position - transform.position).normalized * _moveSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out APlayer player))
        {
            CameraShake.Shake();
            player.TakeDamage(_currentDifficult.BotsDamage);
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
