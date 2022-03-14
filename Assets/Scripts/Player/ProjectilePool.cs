using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    private Projectile _bulletPrefab;
    // Collection checks will throw errors if we try to release an item that is already in the pool.
    private bool collectionChecks = true;
    private int maxPoolSize = 10;
    private IObjectPool<Projectile> _pool;

    public IObjectPool<Projectile> ProjectilesPool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<Projectile>(
                    CreatePooledItem,
                    OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPoolObject,
                    collectionChecks,
                    10,
                    maxPoolSize);
            }

            return _pool;
        }
    }

    private void Awake()
    {
        //TODO load bullet in game manager
        var bulletPrefab = Resources.Load("Projectile") as GameObject;
        _bulletPrefab = bulletPrefab.GetComponent<Projectile>();
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    private void OnDestroyPoolObject(Projectile bullet)
    {
        Destroy(bullet.gameObject);
    }

    // Called when an item is returned to the pool using Release
    private void OnReturnedToPool(Projectile bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    private void OnTakeFromPool(Projectile bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = transform.position;
    }

    private Projectile CreatePooledItem()
    {
        return Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
    }
}
