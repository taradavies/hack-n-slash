using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int playerNumber;
    Controller playerController;

    public bool HasController => playerController != null;
    public int PlayerNumber => playerNumber;

    public void InitialisePlayer(Controller playerController)
    {
        this.playerController = playerController;
        gameObject.name = string.Format("Player {0} - {1}", playerNumber, playerController.gameObject.name);
        Debug.Log(gameObject.name);
    }
}
