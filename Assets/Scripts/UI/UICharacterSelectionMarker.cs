using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectionMarker : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Image _markerImage;
    [SerializeField] Image _lockImage;

    [Tooltip("Offset that centers the marker on the character on the X axis")]
    [SerializeField] int _markerXOffset;
    [Tooltip("Offset so that markers don't overlap on the Y axis")]
    [SerializeField] int _markerYOffset;

    public bool IsLockedIn { get; private set; }
    public bool HasPlayerJoined =>_player.HasController;

    // private variables

    UICharacterSelectionMenu _characterSelectionMenu;
    bool _initialising;
    bool _finishedInitialising;
    void Awake()
    {
        _characterSelectionMenu = GetComponentInParent<UICharacterSelectionMenu>();
        // disabling until player joins
        _markerImage.gameObject.SetActive(false);
        _lockImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!HasPlayerJoined) { return; }

        if (!_initialising)
        {
            StartCoroutine(Initialise());
        }

        if (!_finishedInitialising) { return; }

        if (!IsLockedIn)
        {
            CheckMovingToWizardPanel();
            CheckMovingToVikingPanel();
            CheckCharacterSelected();
        }
        else
        {
            if (_player.PlayerController.attackPressed)
                _characterSelectionMenu.TryStartGame();
        }
    }
    IEnumerator Initialise()
    {
        _initialising = true;
        MoveToCharacterPanel(_characterSelectionMenu.VikingPanel);
        yield return new WaitForSeconds(0.5f);
        _markerImage.gameObject.SetActive(true);
        _finishedInitialising = true;
    }
    void CheckMovingToWizardPanel()
    {
        if (_player.PlayerController.horizontalInput > 0)
        {
            MoveToCharacterPanel(_characterSelectionMenu.WizardPanel);

            if (_markerXOffset > 0)
                _markerXOffset *= -1;
        }
    }
    void CheckMovingToVikingPanel()
    {
        if (_player.PlayerController.horizontalInput < 0)
        {
            MoveToCharacterPanel(_characterSelectionMenu.VikingPanel);

            if (_markerXOffset < 0)
                _markerXOffset *= -1;
        }
    }
    void CheckCharacterSelected()
    {
        if (_player.PlayerController.attackPressed)
        {
            StartCoroutine(LockCharacter());
        }
    }

    IEnumerator LockCharacter()
    {
        _lockImage.gameObject.SetActive(true);
        _markerImage.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        IsLockedIn = true;
    }

    void MoveToCharacterPanel(UICharacterSelectionPanel panel)
    {
        transform.position = panel.transform.position + new Vector3(_markerXOffset, _markerYOffset);
    }
}
