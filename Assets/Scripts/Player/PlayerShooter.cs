using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    private GameDifficult _gameDifficultInstance;
    private GameSettingsSO currentDifficult;

    private HitSpawner _enemyHit;
    private HitSpawner _metalHit;
    private HitSpawner _sandHit;
    private HitSpawner _rockHit;
    private HitSpawner _firstAidKitHit;

    [SerializeField] private AudioSource _shootAudioSource;
    private float _damage;
    private Camera _camera;

    private void Start()
    {
        _gameDifficultInstance = FindObjectOfType<GameDifficult>();
        currentDifficult = _gameDifficultInstance.CurrentDifficult;

        _enemyHit = currentDifficult.EnemyHit;
        _metalHit = currentDifficult.MetalHit;
        _sandHit = currentDifficult.SandHit;
        _rockHit = currentDifficult.RockHit;
        _firstAidKitHit = currentDifficult.FirstAidKitHit;

        _damage = currentDifficult.PlayerDamage;
        _camera = Camera.main;
    }

    private void Update()
    {
        ShootRaycast();
    }

    private void ShootRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            const int rayLength = 10000;

            if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
            {
                if (hit.collider.TryGetComponent(out IDamagable hitObject))
                {
                    hitObject.TakeDamage(_damage);

                    if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Metal")
                    {
                        HitSpawner metalEffect = Instantiate(_metalHit);
                        metalEffect.SpawnEffects(hit);
                        return;
                    }
                    
                    HitSpawner spawned = Instantiate(_enemyHit);
                    spawned.SpawnEffects(hit);
                    
                }
                else if (hit.collider.TryGetComponent(out ICollectableByPlayer collectableObject))
                {
                    collectableObject.CollectByPlayer();
                    
                    HitSpawner spawned = Instantiate(_firstAidKitHit);
                    spawned.SpawnEffects(hit);
                    
                }
                else if (LayerMask.LayerToName(hit.collider.gameObject.layer)  == "Rock")
                {
                    HitSpawner spawned = Instantiate(_rockHit);
                    spawned.SpawnEffects(hit);
                }
                else if (LayerMask.LayerToName(hit.collider.gameObject.layer)  == "Sand")
                {
                    HitSpawner spawned = Instantiate(_sandHit);
                    spawned.SpawnEffects(hit);
                }
                
            }
            
            _shootAudioSource.Play();
        }
    }
}