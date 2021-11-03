using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangeEnemy : AEnemy
{
    [SerializeField] private BulletCreator _bulletCreator;

    private float _movePeriod;
    private float _shotPeriod;
    private bool _canMove;

    private const string _shootTrigger = "Shoot";
    private const string _runLeft = "RunLeft";
    private const string _runRight = "RunRight";

    private void Start()
    {
        moveSpeed = currentDifficult.RangeMoveSpeed;
        _movePeriod = currentDifficult.RangeMovePeriod;
        _shotPeriod = currentDifficult.RangeShotPeriod;
        
        transform.LookAt(player.transform);
        StartCoroutine(StrafeAndShoot());
        moveSpeed = Random.value < 0.5f ? moveSpeed : moveSpeed *= -1;
    }

    private void Update()
    {
        if (_canMove)
        {
            MoveSide();
        }
    }

    private IEnumerator StrafeAndShoot()
    {

        yield return new WaitForSeconds(startDelay);

        while (health > 0)
        {
            moveSpeed *= (-1);
            
            //strafe
            _canMove = true;
            
            if (moveSpeed < 0)
            {
                RunLeftAnimation();
            }
            else
            {
                RunRightAnimation();
            }
            
            
            yield return new WaitForSeconds(Random.Range(1, _movePeriod + 1));
            
            //shoot
            _canMove = false;
            ShootAnimation();
            yield return new WaitForSeconds(Random.Range(1, _shotPeriod + 1));
        }
    }

    private void ShootAnimation()
    {
        animator.SetTrigger(_shootTrigger);
    }

    private void RunLeftAnimation()
    {
        animator.SetTrigger(_runLeft);

    }

    private void RunRightAnimation()
    {
        animator.SetTrigger(_runRight);
    }

    private void MoveSide()
    {
        transform.RotateAround(player.transform.position, Vector3.up, moveSpeed * Time.deltaTime);
        transform.LookAt(player.transform);
    }
    
    //used in animation event
    public void Shoot()
    {
        _bulletCreator.SpawnBullet();
    }
}