using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber;
    public bool HasController => _playerController != null;
    public int PlayerNumber => _playerNumber;
    Controller _playerController;
    UIPlayerText _playerText;

    void Awake()
    {
        _playerText = GetComponentInChildren<UIPlayerText>();
    }

    public void InitialisePlayer(Controller playerController)
    {
        _playerController = playerController;
        gameObject.name = string.Format("Player {0} - {1}", _playerNumber, _playerController.gameObject.name);

        _playerText.HandlePlayerInitialised();
    }
}
