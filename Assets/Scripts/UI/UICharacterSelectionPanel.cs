using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelectionPanel : MonoBehaviour
{
    [SerializeField] Character _characterPrefab;
    public Character CharacterPrefab => _characterPrefab;
}
