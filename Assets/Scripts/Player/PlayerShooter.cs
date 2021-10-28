using UnityEngine;

public class PlayerShooter : APlayer
{
    [SerializeField] private int _damage = 10;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
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
                }

                if (hit.collider.TryGetComponent(out ICollectable collectableObject))
                {
                    collectableObject.Collect(this);
                }

            }
        }
    }

}
