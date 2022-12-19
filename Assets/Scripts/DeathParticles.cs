using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    [SerializeField] PooledMonoBehaviour _deathParticles;
    IDie _character;

    void Awake()
    {
        _character = GetComponent<IDie>();
    }

    void OnEnable()
    {
        _character.OnDie += HandleCharacterDeath;
    }

    void OnDisable()
    {
        _character.OnDie -= HandleCharacterDeath;
    }

    void HandleCharacterDeath(IDie character)
    {
        character.OnDie -= HandleCharacterDeath;

        if (_deathParticles != null)
        {
            Debug.Log("spawning death particle");
            _deathParticles.Get<PooledMonoBehaviour>(character.gameObject.transform.position, Quaternion.identity);

        }
    }
}
