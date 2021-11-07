using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private AudioSource _walkAudioSource;
    [SerializeField] private Animation _animation;
    
    public void PlayWalkSound(AudioClip audioClip)
    {
        _walkAudioSource.pitch = Random.Range(0.7f, 1) * Time.timeScale;
        _walkAudioSource.PlayOneShot(audioClip);
    }

    public void PlayWalkAnimation()
    {
        _animation.Stop();
        _animation.Play("PlayerWalk");
    }
    
    public void PlayIdleAnimation()
    {
        _animation.Stop();
        _animation.Play("PlayerIdle");
    }
    
}
