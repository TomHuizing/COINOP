using UnityEngine;
using UnityEngine.Events;

public class TooltipHandler : MonoBehaviour
{
    [SerializeField] private GameObject tooltipPrefab;
    [SerializeField] private UnityEvent<GameObject> onTooltipShow = new();
    [SerializeField] private UnityEvent onTooltipHide = new();

    public void ShowTooltip()
    {
        GameObject tooltip = Instantiate(tooltipPrefab, TooltipCanvas.Instance.transform);
        onTooltipShow?.Invoke(tooltip);
        TooltipCanvas.Instance.Tooltip = tooltip;
    }

    public void HideTooltip()
    {
        TooltipCanvas.Instance.Tooltip = null;
        onTooltipHide?.Invoke();
    }
}
