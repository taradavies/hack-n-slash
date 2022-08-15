using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakeHit
{
    [SerializeField] GameObject _hitParticles;
    [SerializeField] int _maxHealth = 3;

    int _currentHealth;
    Animator _animator;
    void Awake()
    {
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
