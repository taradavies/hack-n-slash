using UnityEngine;

public class Controller : MonoBehaviour
{
    public int ControllerNumber { get; private set; }
    public bool IsAssigned { get; set; }

    public bool _attack;
    public float _horizontalInput;
    public float _verticalInput;

    string _attackButton;
    bool _attackPressed;
    string _horizontalAxis;
    string _verticalAxis;

    void Update() 
    {
        if (!string.IsNullOrEmpty(_attackButton))
            _attack = Input.GetButton(_attackButton);
            _attackPressed = Input.GetButtonDown(_attackButton);
            _horizontalInput = Input.GetAxis(_horizontalAxis);
            _verticalInput = Input.GetAxis(_verticalAxis);
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
        return _attack;
    }
}
