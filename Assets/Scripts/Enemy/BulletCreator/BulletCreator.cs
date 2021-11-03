using UnityEngine;

public class BulletCreator : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    public void SpawnBullet()
    {
        Instantiate(_bullet, transform);
    }
}
