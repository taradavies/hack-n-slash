using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public int ControllerNumber { get; private set; }
    public bool IsAssigned { get; set; }

    public bool attack;
    public bool attackPressed;
    public float horizontalInput;
    public float verticalInput;

    string _attackButton;
    string _horizontalAxis;
    string _verticalAxis;

    void Update() 
    {
        if (!string.IsNullOrEmpty(_attackButton))
            attack = Input.GetButton(_attackButton);
            attackPressed = Input.GetButtonDown(_attackButton);
            horizontalInput = Input.GetAxis(_horizontalAxis);
            verticalInput = Input.GetAxis(_verticalAxis);
    }

    public Vector3 GetDirection()
    {
        return new Vector3(horizontalInput, 0, verticalInput);
    }

    public void SetControllerNumber(int controllerNumber)
    {
        ControllerNumber = controllerNumber;
        _attackButton = $"P{ControllerNumber}Attack";
        _horizontalAxis = $"P{ControllerNumber}Horizontal";
        _verticalAxis =  $"P{ControllerNumber}Vertical";
    }
    public bool AttackButtonDown()
    {
        return attack;
    }
}
