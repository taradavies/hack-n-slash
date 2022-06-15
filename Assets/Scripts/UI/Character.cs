using UnityEngine;

public class Character : MonoBehaviour
{
    Controller _characterController;
    public void SetController(Controller playerController)
    {
        _characterController = playerController;
    }
}
