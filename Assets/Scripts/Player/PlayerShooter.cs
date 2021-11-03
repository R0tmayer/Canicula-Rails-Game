using UnityEngine;

public class PlayerShooter : APlayer
{
    private float _damage;
    private Camera _camera;

    private void Start()
    {
        base.Start();
        _damage = currentDifficult.PlayerDamage;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            const int rayLength = 10000;

            int meleeLayerIndex = LayerMask.NameToLayer("Melee");
            int rangeLayerIndex = LayerMask.NameToLayer("Range");
            
            int layerMask = (1 << meleeLayerIndex) | (1 << rangeLayerIndex);
            
            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask ))
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
