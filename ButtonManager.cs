using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private PanelManager _panelManager;
    [SerializeField] private Button[] _buttons;

    private void Start()
    {
        SetPanelsListenersToButtons();
    }

    private void SetPanelsListenersToButtons()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int capturedIndex = i;
            _buttons[i].onClick.AddListener(() => OnButtonClicked(capturedIndex));
        }
    }

    private void OnButtonClicked(int index)
    {
        _panelManager.ShowPanelByIndex(index);
    }
}
