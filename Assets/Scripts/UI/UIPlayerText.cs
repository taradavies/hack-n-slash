using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIPlayerText : MonoBehaviour
{
    [SerializeField] float _delay;
    TMP_Text _playerText;
    void Awake()
    {
        _playerText = GetComponent<TMP_Text>();
    }

    public void HandlePlayerInitialised()
    {
        _playerText.SetText("Player Joined!");
        StartCoroutine(ClearTextAfterDelay());
    }

    IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(_delay);
        _playerText.SetText(string.Empty);
    }
}
