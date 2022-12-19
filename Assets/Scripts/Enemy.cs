using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Attacker))]
public class Enemy : PooledMonoBehaviour, ITakeHit, IDie
{
    [SerializeField] PooledMonoBehaviour _hitParticles;
    [SerializeField] int _maxHealth = 3;
    bool IsDead => _currentHealth <= 0;

    NavMeshAgent _navMeshAgent;
    Character _followTarget;
    Animator _animator;
    Attacker _attacker;

    int _currentHealth;

    public event Action<IDie> OnDie;
    public event Action<int, int> OnHealthChange;

    void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    void OnEnable()
    {
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        if (IsDead) {return;}

        if (_followTarget == null)
        {
            StopMovingEnemy();
            _followTarget = GetClosestCharacter();
        }
        else
        {
            if (OutOfAttackRange())
            {
                FollowTarget();
            }
            else
            {
                TryAttack();
            }
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

    bool OutOfAttackRange()
    {
        Vector3 enemy2DPosition = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 target2DPosition = new Vector3(_followTarget.transform.position.x, 0, _followTarget.transform.position.z);
        return Vector3.Distance(enemy2DPosition, target2DPosition) > 1.5f;
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
    public void TakeHit(IAttack hitBy)
    {
        RemoveHealth();

        _animator.SetTrigger("Hit");

        PlayHitParticles();

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void PlayHitParticles()
    {
        _hitParticles.Get<PooledMonoBehaviour>(transform.position + new Vector3(0, 2, 0), Quaternion.identity);
    }

    void RemoveHealth()
    {
        _currentHealth--;
        OnHealthChange?.Invoke(_currentHealth, _maxHealth);
    }

    void Die()
    {
        OnDie?.Invoke(this);

        StopMovingEnemy();
        _animator.SetTrigger("Die");        
        ReturnToPool();
    }

}
