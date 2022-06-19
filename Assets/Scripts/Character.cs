using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] float _moveSpeed;

    Controller _characterController;
    Rigidbody _rb;

    public void SetController(Controller playerController)
    {
        _characterController = playerController;
    }

    void Awake() 
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(_characterController.horizontalInput, 0, _characterController.verticalInput);
        transform.position += (moveDirection * _moveSpeed * Time.deltaTime);
    }
}
