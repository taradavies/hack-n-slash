using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber;
    public bool HasController => PlayerController != null;
    public int PlayerNumber => _playerNumber;
    public Controller PlayerController { get; private set; }
    Controller _playerController;
    UIPlayerText _playerText;

    void Awake()
    {
        _playerText = GetComponentInChildren<UIPlayerText>();
    }

    public void InitialisePlayer(Controller playerController)
    {
        PlayerController = playerController;
        gameObject.name = string.Format("Player {0} - {1}", _playerNumber, PlayerController.gameObject.name);

        _playerText.HandlePlayerInitialised();
    }
}
