using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    Character _character;
    [SerializeField] Image _healthBarForegroundImage;
    void Awake()
    {
        var player = GetComponentInParent<Player>();
        player.OnCharacterChange += Player_OnCharacterChange; // events are still called if a gameobject is inactive
        gameObject.SetActive(false);
    }

    void Player_OnCharacterChange(IDie character)
    {
        _character = character.gameObject.GetComponent<Character>();
        _character.OnHealthChange += HandleHealthChange;
        _character.OnDie += HandleDeath;
        gameObject.SetActive(true);
    }

    void HandleDeath(IDie character)
    {
        // deregistering for events on death because otherwise memory leak
        character.OnHealthChange -= HandleHealthChange;
        character.OnDie -= HandleDeath;

        _character = null; 

        gameObject.SetActive(false);
    }

    void HandleHealthChange(int currentHealth, int maxHealth)
    {
        float percentOfHealth = (float)currentHealth / (float) maxHealth;
        _healthBarForegroundImage.fillAmount = percentOfHealth;
    }
}
