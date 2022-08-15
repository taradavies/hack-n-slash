using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakeHit
{
    Animator _animator;
    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    public void TakeHit(Character hitBy)
    {
        _animator.SetTrigger("Die");
        Destroy(gameObject, 1.2f);
    }
}
