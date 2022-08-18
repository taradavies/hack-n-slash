using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] PooledMonoBehaviour _prefab;
    Queue<PooledMonoBehaviour> _objects = new Queue<PooledMonoBehaviour>();
    public T Get<T>() where T : PooledMonoBehaviour
    {
        if (_objects.Count == 0)
        {
            FillPool();
        }

        return _objects.Dequeue() as T;
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
