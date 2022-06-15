using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    List<Player> _players;

    void Awake() 
    {
        Instance = this;
        _players = FindObjectsOfType<Player>().ToList();
    }

    public void AddPlayerToGame(Controller controller)
    {
        // orders the players by their number to be assigned a controller respectively
        // finds the first one in that list in that order that doesn't have a controller assigned
        var firstNonActivePlayer = _players
        .OrderBy(t => t.PlayerNumber)
        .FirstOrDefault(t => !t.HasController);
        
        firstNonActivePlayer.InitialisePlayer(controller);
    }

    public void SpawnPlayerCharacters()
    {
        foreach (var player in _players)
        {
            if (player.HasController && player.CharacterPrefab != null)
                player.SpawnCharacter();
        }
    }
}
