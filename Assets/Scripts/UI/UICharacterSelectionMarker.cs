using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectionMarker : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Image _markerImage;
    [SerializeField] Image _lockImage;

    [Tooltip("Offset that centers the marker on the character")]
    [SerializeField] int _markerXOffset;
    [SerializeField] int _markerYOffset;

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
        if (!_player.HasController) {return;}

        if (!_initialising)
        {
            StartCoroutine(Initialise());
        }

        if (!_finishedInitialising) {return;}

        if (_player.PlayerController._horizontalInput > 0)
        {
            MoveToCharacterPanel(_characterSelectionMenu.WizardPanel);

            if (_markerXOffset > 0)
                _markerXOffset *= -1;
        }

        else if (_player.PlayerController._horizontalInput < 0)
        {
            MoveToCharacterPanel(_characterSelectionMenu.VikingPanel);

            if (_markerXOffset < 0)
                _markerXOffset *= -1;
        }
    }

    void MoveToCharacterPanel(UICharacterSelectionPanel panel)
    {
        transform.position = panel.transform.position + new Vector3(_markerXOffset, _markerYOffset);
    }

    IEnumerator Initialise()
    {
        _initialising = true;
        MoveToCharacterPanel(_characterSelectionMenu.VikingPanel);
        yield return new WaitForSeconds(0.5f);
        _markerImage.gameObject.SetActive(true);
        _finishedInitialising = true;
    }
}
