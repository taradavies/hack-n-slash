using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ITakeHit
{
    [SerializeField] GameObject _hitParticles;
    [SerializeField] int _maxHealth = 3;

    bool IsDead => _currentHealth <= 0;
   
    NavMeshAgent _navMeshAgent;
    Character _followTarget;
    int _currentHealth;
    Animator _animator;

    void Update()
    {
        if (IsDead) {return;}

        if (_followTarget == null)
        {
            _followTarget = GetClosestCharacter();
            _animator.SetFloat("Speed", 0f);
            _navMeshAgent.isStopped = true;
        }
        else
        {
            if (Vector3.Distance(transform.position, _followTarget.transform.position) > 2) 
            {
                _navMeshAgent.isStopped = false;
                _animator.SetFloat("Speed", 1f);
                _navMeshAgent.SetDestination(_followTarget.transform.position);
            }
            else
            {
                // Attack()
            }
        }
    }

    Character GetClosestCharacter()
    {
        return Character.AllCharactersInScene
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault();
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
        _navMeshAgent.isStopped = true;
        Destroy(gameObject, 1.2f);
    }
}
