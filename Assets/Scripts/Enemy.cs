using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private PlayerMover _player;
    private int _health = 1;
    public event Action<Enemy> Died;

    private Vector3 _direction;
    private bool _isShooting;
    
    private void Awake()
    {
        _player = FindObjectOfType<PlayerMover>();
    }

    private void Start()
    {
        StartCoroutine(StrafeAndShoot());
        _moveSpeed = Random.value < 0.5f ? _moveSpeed : _moveSpeed *= -1;
    }

    private void Update()
    {
        transform.LookAt(_player.transform);
        
        if (!_isShooting)
        {
            MoveSide();
        }

    }

    private IEnumerator StrafeAndShoot()
    {
        while (_health > 0)
        {
            var randomValue = Random.Range(1, 3);
            _moveSpeed *= (-1);
            
            _isShooting = true;
            Shoot();
            yield return new WaitForSeconds(randomValue);
            _isShooting = false;
            yield return new WaitForSeconds(randomValue);
        }
    }

    private void Shoot()
    {
        //Shoot anim
    }

    private void MoveSide()
    {
        transform.RotateAround(_player.transform.position, Vector3.up, _moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Died?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}