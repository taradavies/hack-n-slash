using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, ITakeHit
{
    [SerializeField] float _forceAmount = 10f;
     Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void TakeHit(Character hitBy)
    {
        var direction = Vector3.Normalize(transform.position - hitBy.transform.position);
        _rb.AddForce(direction * _forceAmount, ForceMode.Impulse);
        
    }
}
