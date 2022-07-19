using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] Vector3 _spawnPoint;

    public Vector3 SpawnPoint => _spawnPoint;

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
        Vector3 moveDirection = _characterController.GetDirection();
        if (moveDirection.magnitude >= 0.1)
        {
            transform.position += (moveDirection * _moveSpeed * Time.deltaTime);
            // sets the rotation of the character
            transform.forward = moveDirection * 360f;
        }
    }
}
