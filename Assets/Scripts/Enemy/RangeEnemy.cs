using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangeEnemy : AEnemy
{
    private bool _isShooting;
    
    private void Start()
    {
        StartCoroutine(StrafeAndShoot());
        moveSpeed = Random.value < 0.5f ? moveSpeed : moveSpeed *= -1;
    }

    private void Update()
    {
        transform.LookAt(player.transform);
        
        if (!_isShooting)
        {
            MoveToTheSide();
        }
    }

    private IEnumerator StrafeAndShoot()
    {
        while (health > 0)
        {
            var randomValue = Random.Range(1, 3);
            moveSpeed *= (-1);
            
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

    private void MoveToTheSide()
    {
        transform.RotateAround(player.transform.position, Vector3.up, moveSpeed * Time.deltaTime);
    }


}