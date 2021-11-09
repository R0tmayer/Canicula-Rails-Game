using UnityEngine;

public class HitSpawner : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private AudioSource _audioSource;
    private ParticleSystem[] _particles;

    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _audioSource = GetComponentInChildren<AudioSource>();
        _particles = GetComponentsInChildren<ParticleSystem>();
    }

    public void SpawnEffects(RaycastHit hit)
    {
        if (_sprite != null)
        {
            Quaternion hitRotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            Instantiate(_sprite, hit.point - (hit.point * 0.001f), hitRotation);
        }

        if (_audioSource != null && _audioSource.clip != null)
        {
            _audioSource.Play();
        }

        if (_particles != null)
        {
            foreach (ParticleSystem particle in _particles)
            {
                particle.transform.position = hit.point;
                particle.Play();
            }
        }
    }
    
    public void SpawnEffects()
    {
        if (_audioSource != null && _audioSource.clip != null)
        {
            _audioSource.Play();
        }

        if (_particles != null)
        {
            foreach (ParticleSystem particle in _particles)
            {
                particle.Play();
            }
        }
  
    }
}
