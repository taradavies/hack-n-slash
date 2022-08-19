using System;
using UnityEngine;

public class PooledMonoBehaviour : MonoBehaviour
{
    [SerializeField] int _maxNumberOfObjects = 20;
    public int MaxNumberOfObjects => _maxNumberOfObjects;
    public event Action<PooledMonoBehaviour> OnReturnToPool;

    void OnDisable()
    {
        if (OnReturnToPool != null)
        {
            OnReturnToPool(this);
        }
    }

    public T Get<T>(bool enable = true) where T : PooledMonoBehaviour
    {
        var pool = Pool.GetPool(this);
        var pooledObject = pool.Get<T>();

        if (enable) { pooledObject.gameObject.SetActive(true); }

        return pooledObject;
    }

    public T Get<T>(Vector3 position, Quaternion rotation) where T : PooledMonoBehaviour
    {
        var pooledObject = Get<T>();

        pooledObject.transform.position = position;
        pooledObject.transform.rotation = rotation;

        return pooledObject;
    }
}
