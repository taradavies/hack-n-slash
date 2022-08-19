using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] PooledMonoBehaviour _prefab;

    static Dictionary<PooledMonoBehaviour, Pool> _pools = new Dictionary<PooledMonoBehaviour, Pool>();
    Queue<PooledMonoBehaviour> _objects = new Queue<PooledMonoBehaviour>();

    public T Get<T>() where T : PooledMonoBehaviour
    {
        if (_objects.Count == 0)
        {
            FillPool();
        }

        return _objects.Dequeue() as T;
    }

    public static Pool GetPool(PooledMonoBehaviour prefab)
    {
        if (_pools.ContainsKey(prefab))
            return _pools[prefab];

        var pool = CreatePool(prefab);
        _pools.Add(prefab, pool);
        
        return pool;
    }

    static Pool CreatePool(PooledMonoBehaviour prefab)
    {
        var poolGameObject = new GameObject("Pool-" + prefab.gameObject.name);
        var pool = poolGameObject.AddComponent<Pool>();
        pool._prefab = prefab;
        return pool;
    }

    void FillPool()
    {
        for (int i = 0; i < _prefab.MaxNumberOfObjects; i++)
        {
            var pooledObject = Instantiate(_prefab) as PooledMonoBehaviour;
            pooledObject.gameObject.name += " " + i;
            
            pooledObject.OnReturnToPool += AddObjectToAvailableQueue;

            pooledObject.transform.SetParent(this.transform);
            pooledObject.gameObject.SetActive(false);
        }
        
    }
    void AddObjectToAvailableQueue(PooledMonoBehaviour pooledObject)
    {
        pooledObject.transform.SetParent(this.transform);
        _objects.Enqueue(pooledObject);
    }
}
