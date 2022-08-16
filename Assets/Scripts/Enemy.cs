using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ITakeHit
{
    [SerializeField] GameObject _hitParticles;
    [SerializeField] int _maxHealth = 3;
   
    NavMeshAgent _navMeshAgent;
    Character _followTarget;
    int _currentHealth;
    Animator _animator;

    void Update()
    {
        if (_followTarget == null)
        {
            _followTarget = FindObjectOfType<Character>();
        }
        else
        {
            _navMeshAgent.SetDestination(_followTarget.transform.position);
        }
    }
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    // performance benefit because we don't want to constantly pool them
    void OnEnable()
    {
        _currentHealth = _maxHealth;
    }
    public void TakeHit(Character hitBy)
    {
        _currentHealth --;

        _animator.SetTrigger("Hit");
        Instantiate(_hitParticles, transform.position + new Vector3(2, 2, -0.5f), Quaternion.identity);    

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _animator.SetTrigger("Die");
        Destroy(gameObject, 1.2f);
    }
}
