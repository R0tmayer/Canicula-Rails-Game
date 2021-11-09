using UnityEngine;

public class HitSpawner : MonoBehaviour
{
    private ParticleSystem[] _particles;

    private void Start()
    {
        _particles = GetComponentsInChildren<ParticleSystem>();
    }
}
