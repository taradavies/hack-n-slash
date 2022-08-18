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
}
