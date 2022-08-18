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
    public void TakeHit(IAttack hitBy)
    {
        var direction = Vector3.Normalize(transform.position - hitBy.Transform.position);
        _rb.AddForce(direction * _forceAmount, ForceMode.Impulse);
        
    }
}
