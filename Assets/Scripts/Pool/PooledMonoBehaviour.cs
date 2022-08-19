using System;
using System.Collections;
using UnityEngine;

public class PooledMonoBehaviour : MonoBehaviour
{
    [SerializeField] int _maxNumberOfObjects = 20;
    [SerializeField] float _returnToPoolDelay = 5f;

    public int MaxNumberOfObjects => _maxNumberOfObjects;
    public event Action<PooledMonoBehaviour> OnReturnToPool;

    void OnDisable()
    {
        if (OnReturnToPool != null)
        {
            OnReturnToPool(this);
        }
    }

    IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    // accessible to the subclass - enemy
    protected void ReturnToPool()
    {
        StartCoroutine(ReturnToPoolAfterDelay(_returnToPoolDelay));
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
