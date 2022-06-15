using TMPro;
using UnityEngine;

public class UICharacterSelectionMenu : MonoBehaviour
{
    [SerializeField] UICharacterSelectionPanel _vikingPanel;
    [SerializeField] UICharacterSelectionPanel _wizardPanel;

    [SerializeField] TMP_Text _startGameText;
    public UICharacterSelectionPanel VikingPanel => _vikingPanel;
    public UICharacterSelectionPanel WizardPanel => _wizardPanel;
    bool _startEnabled;
    void Update()
    {
        int playersInCount = 0;
        int playersLockedCount = 0;

        var markers = GetComponentsInChildren<UICharacterSelectionMarker>();

        foreach (var marker in markers)
        {
            if (marker.IsLockedIn)
                playersLockedCount++;
            
            if (marker.HasPlayerJoined)
                playersInCount++;
        }

        _startEnabled = playersInCount > 0 && playersInCount == playersLockedCount;
        _startGameText.gameObject.SetActive(_startEnabled);
    }

    public void TryStartGame()
    {
        if (_startEnabled)
        {
            GameManager.Instance.Begin();
            gameObject.SetActive(false);
        }
    }
}
