using System;
using UnityEngine;

public class FirstAidKit : MonoBehaviour, ICollectableByPlayer
{
    private GameDifficult _gameDifficultInstance;
    private GameSettingsSO currentDifficult;
    private PlayerLife _player;
    
    private float _healingValue;
    private float _zAngle;

    private void Start()
    {
        _gameDifficultInstance = FindObjectOfType<GameDifficult>();
        _player = FindObjectOfType<PlayerLife>();
        
        currentDifficult = _gameDifficultInstance.CurrentDifficult;
        
        _healingValue = currentDifficult.FirstAidKitHeal;
        _zAngle = transform.localEulerAngles.z;
    }

    private void Update()
    {
        Vector3 lookPos = _player.transform.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
        var eulerY = lookRot.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler (0, eulerY, _zAngle);
        transform.rotation = rotation;
    }

    public void CollectByPlayer()
    {
        _player.TakeHeal(_healingValue);
        gameObject.SetActive(false);
    }
}
