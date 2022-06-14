using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int ControllerNumber { get; private set; }
    public bool IsAssigned { get; set; }

    string attackButton;
    public bool attack;

    void Update() 
    {
        if (!string.IsNullOrEmpty(attackButton))
            attack = Input.GetButton(attackButton);
    }

    public void SetControllerNumber(int controllerNumber)
    {
        ControllerNumber = controllerNumber;
        attackButton = $"P{ControllerNumber}Attack";
    }

    public bool AttackButtonDown()
    {
        return attack;
    }
}
