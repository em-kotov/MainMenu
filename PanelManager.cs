using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] _panels;
    [SerializeField] private int _mainMenuIndex;

    private void Start()
    {
        HideAllPanels();
        SetPanelActive(_panels[_mainMenuIndex], true);
    }

    public void ShowPanelByIndex(int index)
    {
        if (_panels.Length > index && index >= 0)
        {
            HideAllPanels();
            SetPanelActive(_panels[index], true);
        }
    }

    private void HideAllPanels()
    {
        foreach (var panel in _panels)
        {
            SetPanelActive(panel, false);
        }
    }

    private void SetPanelActive(CanvasGroup panel, bool isActive)
    {
        int panelAlphaVisible = 1;
        int panelAlphaTransparent = 0;

        panel.alpha = isActive ? panelAlphaVisible : panelAlphaTransparent;
        panel.interactable = isActive;
        panel.blocksRaycasts = isActive;

        EnableActivatablePanel(panel, isActive);
    }

    private void EnableActivatablePanel(CanvasGroup panel, bool isActive)
    {
        if (panel.TryGetComponent(out IActivatablePanel activatablePanel))
        {
            if (isActive)
            {
                activatablePanel.OnPanelActivated();
            }
            else
            {
                activatablePanel.OnPanelDeactivated();
            }
        }
    }
}
