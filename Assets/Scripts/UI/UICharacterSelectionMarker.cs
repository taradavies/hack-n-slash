using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectionMarker : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Image _markerImage;
    [SerializeField] Image _lockImage;
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

        // check for player controls and selection + locking character
    }

    IEnumerator Initialise()
    {
        _initialising = true;
        yield return new WaitForSeconds(0.5f);
        _markerImage.gameObject.SetActive(true);
        _finishedInitialising = true;
    }
}
