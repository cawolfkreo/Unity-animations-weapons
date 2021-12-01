using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script by: Camilo Zambrano
/// </summary>
public class ObjectPool
{
    /// <summary>
    /// This is the queue where the object pool
    /// will store the bullets for the gun
    /// </summary>
    private Queue<GameObject> _bulletsPool;

    /// <summary>
    /// This is is the flag used to know the
    /// behaviour of the object pool when all of
    /// the objects on the pool are being used.
    /// </summary>
    private bool _increasePoolSize;

    /// <summary>
    /// This is the prefab used by the pool to
    /// instanciate the game object it stores.
    /// </summary>
    private GameObject _prefabForThePool;

    /// <summary>
    /// This is the life time for each object
    /// instantiated for the pool.
    /// </summary>
    private float _projectileLifeTime;

    /// <summary>
    /// This is the radious where the effects of
    /// the projectile extend.
    /// </summary>
    private float _radiousEffect;

    /// <summary>
    /// This is how strong are the effects of the
    /// projectile.
    /// </summary>
    private float _effectStrength;

    /// <summary>
    /// This is the GameObject where the pool will
    /// be stored in the scene's hierarchy.
    /// </summary>
    private Transform _poolParent;

    /// <summary>
    /// This creates an object pool that stores
    /// instances of the specified game object.
    /// </summary>
    /// <param name="prefabForThePool">The prefab used to create the instances of the game object to store</param>
    /// <param name="poolSize">The initial size of the pool</param>
    /// <param name="IncreasePoolSize">How the pool will behave if it is full. True to increase the pool size and false to re-use old objects.</param>
    /// <param name="projectileLifeTime">The lifetime for each pooled object.</param>
    public ObjectPool(GameObject prefabForThePool, int poolSize, bool IncreasePoolSize, float projectileLifeTime, float radiousEffect, float effectStrength)
    {
        _bulletsPool = new Queue<GameObject>();
        _increasePoolSize = IncreasePoolSize;
        _prefabForThePool = prefabForThePool;
        _projectileLifeTime = projectileLifeTime;
        _radiousEffect = radiousEffect;
        _effectStrength = effectStrength;

        GameObject poolObject = new GameObject($"{prefabForThePool.name}'s pool");
        _poolParent = poolObject.transform;

        for (int i = 0; i < poolSize; i++)
        {
            _ = InstanceAnObject();
        }
    }

    /// <summary>
    /// This method creates a new instance for the
    /// prefab and adds it into the pool. it
    /// returns the newly created object.
    /// </summary>
    /// <returns>The new created game object created</returns>
    private GameObject InstanceAnObject()
    {
        GameObject objectInstance = GameObject.Instantiate(_prefabForThePool);
        BasicPoolProjectile basic = objectInstance.GetComponent<BasicPoolProjectile>();
        basic.TimeIsEnabled = _projectileLifeTime;
        basic.SetAttractValues(_radiousEffect, _effectStrength);
        objectInstance.transform.parent = _poolParent;
        objectInstance.SetActive(false);

        _bulletsPool.Enqueue(objectInstance);

        return objectInstance;
    }

    /// <summary>
    /// This will return an object from the pool to
    /// be used by another script.
    /// </summary>
    /// <returns>The instance of the object to use</returns>
    public GameObject GetAnInstance()
    {
        GameObject gameObject = _bulletsPool.Dequeue();

        if (gameObject.activeSelf && _increasePoolSize)
        {
            _bulletsPool.Enqueue(gameObject);
            gameObject = InstanceAnObject();
        }
        else
        {
            gameObject.SetActive(false);
            _bulletsPool.Enqueue(gameObject);
        }

        gameObject.SetActive(true);

        return gameObject;
    }
}