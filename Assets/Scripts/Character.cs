using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(Rigidbody))]

public class Character : MonoBehaviour, ITakeHit, IDie
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] Vector3 _spawnPoint;
    [SerializeField] int _maxHealth = 10;
    [Tooltip("Value for controllers ie. if the joystick is not perfectly centered but nothings been moved, this is a value to factor in error.")]
    [SerializeField] float _controllerResistanceConstant = 0.25f;
    [SerializeField] float _rotationSpeed = 720;

    public Vector3 SpawnPoint => _spawnPoint;
    public static List<Character> AllCharactersInScene = new List<Character>();
    public event Action<IDie> OnDie = delegate {};
    public event Action<int, int> OnHealthChange;

    Controller _characterController;
    Animator _animationController;
    Rigidbody _rb;
    Attacker _attacker;

    int _currentHealth;

    public void SetController(Controller playerController)
    {
        _characterController = playerController;
    }

    void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
        _animationController = GetComponentInChildren<Animator>();
        _attacker = GetComponent<Attacker>();
    }

    void Update()
    {
        Vector3 moveDirection = _characterController.GetDirection();

        if (moveDirection.magnitude > _controllerResistanceConstant)
        {
            var velocity = moveDirection * _moveSpeed;
            _rb.velocity = velocity;
            // RotateCharacter(moveDirection);

            _animationController.SetFloat("Speed", Mathf.Clamp(moveDirection.magnitude, 0, 1));
        }
        else
        {
            _animationController.SetFloat("Speed", 0);
        }

        if (_characterController.attackPressed)
        {
            if (_attacker.CanAttack)
            {
                _animationController.SetTrigger("Attack");
            }
        }
    }

    void RotateCharacter(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotate = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, _rotationSpeed * Time.deltaTime);
        }
            
    }
    
    void OnEnable()
    {
        _currentHealth = _maxHealth;
        if (!AllCharactersInScene.Contains(this))
        {
            AllCharactersInScene.Add(this);
        }
    }
    void OnDisable()
    {
        if (AllCharactersInScene.Contains(this))
        {
            AllCharactersInScene.Remove(this);
        }
    }
    public void TakeHit(IAttack hitBy)
    {
        // makes sure the OnDie event is not repeatedly invoked if health is below zero. 
        if (_currentHealth <= 0)
            return;

        _currentHealth -= hitBy.Damage;

        OnHealthChange(_currentHealth, _maxHealth);
        
        if (_currentHealth <= 0) { 
            Die();
        }
    }

    void Die()
    {
        OnDie?.Invoke(this);
    }
}
