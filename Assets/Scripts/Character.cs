using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] Vector3 _spawnPoint;
    [SerializeField] float _attackOffset = 1f;
    [SerializeField] float _attackRadius = 1f;


    public Vector3 SpawnPoint => _spawnPoint;

    Controller _characterController;
    Animator _animationController;
    Rigidbody _rb;
    Collider[] _attackResults;

    public void SetController(Controller playerController)
    {
        _characterController = playerController;
    }

    void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
        _animationController = GetComponentInChildren<Animator>();
        _attackResults = new Collider[10];
    }
    void Update()
    {
        Vector3 moveDirection = _characterController.GetDirection();
        transform.position += (moveDirection * _moveSpeed * Time.deltaTime);
        transform.forward = moveDirection * 360f;
        AnimatePlayerMovement(moveDirection);
        
        if (_characterController.attackPressed)
        {
            Attack();
        }
    }

    void AnimatePlayerMovement(Vector3 moveDirection)
    {
        _animationController.SetFloat("MoveX", moveDirection.x);
        _animationController.SetFloat("MoveY", moveDirection.z);
    }
    
    private void Attack()
    {
        _animationController.SetTrigger("Attack");

        Vector3 position = transform.position + transform.forward * _attackOffset;
        // returns what is in the area defined by the sphere
        int hitCount = Physics.OverlapSphereNonAlloc(position, _attackRadius, _attackResults);

        for (int i = 0; i < hitCount; i++)
        {
            var box = _attackResults[i].GetComponent<Box>();
            if (box != null)
            {
                box.TakeHit(this);
            }
        }
    }
}
