using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber;
    

    public bool HasController => PlayerController != null;
    public int PlayerNumber => _playerNumber;
    public Controller PlayerController { get; private set; }

    public Character CharacterPrefab { get; set; }
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

    public void SpawnCharacter()
    {
        // changed vector3 with a spawn point
        var character = Instantiate(CharacterPrefab, CharacterPrefab.SpawnPoint, Quaternion.identity);
        character.SetController(PlayerController);
    }
}
