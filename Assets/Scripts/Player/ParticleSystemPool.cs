using UnityEngine;
using UnityEngine.Pool;

// This component returns the particle system to the pool when the OnParticleSystemStopped event is received.
[RequireComponent(typeof(ParticleSystem))]
public class ReturnToPool : MonoBehaviour
{
    public ParticleSystem system;
    public IObjectPool<ParticleSystem> pool;

    void Start()
    {
        system = GetComponent<ParticleSystem>();
        var main = system.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        Debug.Log("Particle Stopped");
        // Return to the pool
        pool.Release(system);
    }
}

// This example spans a random number of ParticleSystems using a pool so that old systems can be reused.
public class ParticleSystemPool : MonoBehaviour
{
    [Tooltip("Particle system prefab that will be pooled")]
    public ParticleSystem _particleSystem;

    public enum PoolType
    {
        Stack,
        LinkedList
    }

    public PoolType poolType;

    // Collection checks will throw errors if we try to release an item that is already in the pool.
    public bool collectionChecks = true;
    public int maxPoolSize = 10;

    IObjectPool<ParticleSystem> _pool;

    public IObjectPool<ParticleSystem> Pool
    {
        get
        {
            if (_pool == null)
            {
                if (poolType == PoolType.Stack)
                    _pool = new ObjectPool<ParticleSystem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                else
                    _pool = new LinkedPool<ParticleSystem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
            }
            return _pool;
        }
    }

    ParticleSystem CreatePooledItem()
    {
        //var go = new GameObject("Pooled Particle System");
        //var ps = go.AddComponent<ParticleSystem>();
        //var go = _particleSystem;
        //var ps = go.GetComponent<ParticleSystem>();
        var ps = Instantiate(_particleSystem, transform.position, Quaternion.Euler(90, 0, 0));
        ps.transform.parent = transform;
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        var main = ps.main;
        //main.duration = 1;
        //main.startLifetime = 1;
        main.loop = false;

        // This is used to return ParticleSystems to the pool when they have stopped.
        var returnToPool = ps.gameObject.AddComponent<ReturnToPool>();
        returnToPool.pool = Pool;

        return ps;
    }

    // Called when an item is returned to the pool using Release
    void OnReturnedToPool(ParticleSystem system)
    {
        system.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(ParticleSystem system)
    {
        system.gameObject.SetActive(true);
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(ParticleSystem system)
    {
        Destroy(system.gameObject);
    }
}
