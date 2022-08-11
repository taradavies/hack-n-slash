using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] Vector3 _spawnPoint;
    public Vector3 SpawnPoint => _spawnPoint;

    Controller _characterController;
    Animator _animationController;
    Rigidbody _rb;

    public void SetController(Controller playerController)
    {
        _characterController = playerController;
    }

    void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
        _animationController = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector3 moveDirection = _characterController.GetDirection();
        transform.position += (moveDirection * _moveSpeed * Time.deltaTime);
        transform.forward = moveDirection * 360f;
        AnimatePlayerMovement(moveDirection);
        
        if (_characterController.attackPressed)
        {
            _animationController.SetTrigger("Attack");
        }
    }

    void AnimatePlayerMovement(Vector3 moveDirection)
    {
        _animationController.SetFloat("MoveX", moveDirection.x);
        _animationController.SetFloat("MoveY", moveDirection.z);
    }
}
