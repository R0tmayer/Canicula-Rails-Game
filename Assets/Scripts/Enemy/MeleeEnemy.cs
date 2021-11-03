using System.Collections;
using UnityEngine;

public class MeleeEnemy : AEnemy
{
    private float _stopDistance;
    private string _punchTrigger = "Punch";
    
    private void Start()
    {
        moveSpeed = currentDifficult.MeleeMoveSpeed;
        _stopDistance = currentDifficult.MeleeStopDistance;
        transform.LookAt(player.transform);
        StartCoroutine(MoveToPlayer());
    }

    private IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(startDelay);
        
        RunAnimation();
        
        Vector3 playerPosition =  new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) ;
        var distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        
        while (distanceToPlayer >= _stopDistance && health > 0)
        {
            transform.LookAt(playerPosition);
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
            distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
            yield return null;
        }

        PunchAnimation();
    }

    private void PunchAnimation()
    {
        animator.SetTrigger(_punchTrigger);
    }

    private void Punch()
    {
        player.TakeDamage(currentDifficult.BotsDamage);
    }
}
