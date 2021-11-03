using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    private const float _turnSpeed = 2;
    private const float _timeToNextShake = 2;
    
    private Camera _camera;
    private static Vector3 _cameraTurn = Vector3.zero;
    private static float _shakeTimer;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {

        _shakeTimer += Time.deltaTime;
        
        if ( _cameraTurn != Vector3.zero )
        {
            _cameraTurn = Vector3.Slerp(_cameraTurn, Vector3.zero, Time.deltaTime * _turnSpeed);

            _camera.transform.localEulerAngles = _cameraTurn;
        }
        else if ( _camera.transform.localEulerAngles != Vector3.zero )
        {
            _camera.transform.localEulerAngles = Vector3.zero;
        }
    }

    public static void Shake()
    {

        if (_shakeTimer >= _timeToNextShake)
        {
            var hurtEffectPosition = new Vector2(Random.Range(-300, 300), Random.Range(-200, 200));

            _cameraTurn = new Vector3(hurtEffectPosition.x,
                hurtEffectPosition.y, hurtEffectPosition.x * 0.3f) * 0.1f;

            _shakeTimer = 0;
        }
    }
}