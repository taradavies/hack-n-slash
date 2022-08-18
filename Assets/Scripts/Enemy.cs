using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Attacker))]
public class Enemy : MonoBehaviour, ITakeHit
{
    [SerializeField] GameObject _hitParticles;
    [SerializeField] int _maxHealth = 3;

    bool IsDead => _currentHealth <= 0;

    NavMeshAgent _navMeshAgent;
    Character _followTarget;
    Animator _animator;
    Attacker _attacker;

    int _currentHealth;

    void Update()
    {
        if (IsDead) {return;}

        if (_followTarget == null)
        {
            _followTarget = GetClosestCharacter();
            StopMovingEnemy();
        }
        else
        {
            if (Vector3.Distance(transform.position, _followTarget.transform.position) > 1.5f)
            {
                FollowTarget();
            }
            else
            {
                TryAttack();
            }
        }
    }

    void FollowTarget()
    {
        _navMeshAgent.isStopped = false;
        _animator.SetFloat("Speed", 1f);
        _navMeshAgent.SetDestination(_followTarget.transform.position);
    }

    void TryAttack()
    {
        StopMovingEnemy();
        if (_attacker.CanAttack)
        {
            _animator.SetTrigger("Attack");
            _attacker.Attack(_followTarget);
        }
    }

    void StopMovingEnemy()
    {
        _animator.SetFloat("Speed", 0f);
        _navMeshAgent.isStopped = true;
    }

    Character GetClosestCharacter()
    {
        return Character.AllCharactersInScene
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
    }

    void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    // performance benefit because we don't want to constantly pool them
    void OnEnable()
    {
        _currentHealth = _maxHealth;
    }
    public void TakeHit(IAttack hitBy)
    {
        _currentHealth --;

        _animator.SetTrigger("Hit");
        Instantiate(_hitParticles, transform.position + new Vector3(2, 2, -0.5f), Quaternion.identity);    

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        StopMovingEnemy();
        _animator.SetTrigger("Die");
        Destroy(gameObject, 1.2f);
    }
}
