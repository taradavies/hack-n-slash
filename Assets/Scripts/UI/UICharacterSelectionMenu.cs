using UnityEngine;

public class UICharacterSelectionMenu : MonoBehaviour
{
    [SerializeField] UICharacterSelectionPanel _vikingPanel;
    [SerializeField] UICharacterSelectionPanel _wizardPanel;

    public UICharacterSelectionPanel VikingPanel => _vikingPanel;
    public UICharacterSelectionPanel WizardPanel => _wizardPanel;
}
