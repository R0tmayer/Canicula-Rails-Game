using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private GameDifficult _gameDifficultInstance;
    private GameSettingsSO currentDifficult;
    private StatsManager _statsManager;


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
        _statsManager = FindObjectOfType<StatsManager>();
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
        if (!UIManager.IS_INPUTED)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            const int rayLength = 1000;

            if (Physics.Raycast(ray, out RaycastHit hit, rayLength))
            {
                
                if (hit.collider.TryGetComponent(out IDamagable hitObject))
                {
                    hitObject.TakeDamage(_damage);

                    if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Metal")
                    {
                        CreateEffect(_metalHit, hit);

                        return;
                    }
                    
                    CreateEffect(_enemyHit, hit);
                    _statsManager.IncreaseTotalHits();
                    
                }
                else if (hit.collider.TryGetComponent(out ICollectableByPlayer collectableObject))
                {
                    collectableObject.CollectByPlayer();
                    
                    CreateEffect(_firstAidKitHit, hit);
                }
                else if (LayerMask.LayerToName(hit.collider.gameObject.layer)  == "Rock")
                {
                    CreateEffect(_rockHit, hit);

                }
                else if (LayerMask.LayerToName(hit.collider.gameObject.layer)  == "Sand")
                {
                    CreateEffect(_sandHit, hit);

                }
                else if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Metal")
                {
                    CreateEffect(_metalHit, hit);
                    return;
                }
                
            }
            
            _shootAudioSource.Play();
        }
    }

    private void CreateEffect(HitSpawner effect, RaycastHit hit)
    {
        HitSpawner spawned = Instantiate(effect,hit.point - hit.point * 0.001f, Quaternion.FromToRotation(Vector3.forward, hit.normal));
    }
}