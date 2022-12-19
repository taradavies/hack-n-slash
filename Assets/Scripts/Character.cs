using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Attacker))]
public class Character : MonoBehaviour, ITakeHit, IDie
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] Vector3 _spawnPoint;
    [SerializeField] int _maxHealth = 10;

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
        transform.position += (moveDirection * _moveSpeed * Time.deltaTime);

        if (moveDirection.x != 0 || moveDirection.z != 0)
            transform.forward = moveDirection * 360f;

        AnimatePlayerMovement(moveDirection);
        
        if (_characterController.attackPressed)
        {
            if (_attacker.CanAttack)
            {
                Debug.Log("Attack.");
                _animationController.SetTrigger("Attack");
            }
        }
    }
    void AnimatePlayerMovement(Vector3 moveDirection)
    {
        _animationController.SetFloat("MoveX", moveDirection.x);
        _animationController.SetFloat("MoveY", moveDirection.z);
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
