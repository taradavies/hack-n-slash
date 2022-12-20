using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    CinemachineTargetGroup _targetGroup;

    void Awake()
    {
        _targetGroup = GetComponent<CinemachineTargetGroup>();

        var players = FindObjectsOfType<Player>();

        foreach (var player in players)
        {
            // the parameter (character) is gonna get assigned to a variable character
            player.OnCharacterChange += (character) => HandleCharacterChange(player, character);
        }
    }

    void HandleCharacterChange(Player player, Character character)
    {
        int playerIndex = player.PlayerNumber - 1;
        _targetGroup.m_Targets[playerIndex].target = character.transform;
    }
}
