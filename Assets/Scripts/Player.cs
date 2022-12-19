using System;
using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber;
    [SerializeField] float _respawnTime = 5;
    
    public event Action<Character> OnCharacterChange = delegate {};

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

        character.OnDied += HandleCharacterDeath;

        OnCharacterChange(character);
    }

    void HandleCharacterDeath(Character character)
    {
        character.OnDied -= HandleCharacterDeath;

        Destroy(character.gameObject);

        StartCoroutine(RespawnAfterDelay());
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(_respawnTime);
        SpawnCharacter();
    }
}
