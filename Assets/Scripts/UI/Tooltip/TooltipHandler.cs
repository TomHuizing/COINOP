using UnityEngine;
using UnityEngine.Events;

public class TooltipHandler : MonoBehaviour
{
    [SerializeField] private GameObject tooltipPrefab;
    [SerializeField] private UnityEvent<GameObject> onTooltipShow = new();
    [SerializeField] private UnityEvent onTooltipHide = new();

    private TooltipManager tooltipManager;

    public string Text { get; set; }

    void Start()
    {
        // tooltipManager = TooltipManager.Instance;
    }

    public void ShowTooltip()
    {
        // GameObject tooltip = Instantiate(tooltipPrefab, TooltipCanvas.Instance.transform);
        // onTooltipShow?.Invoke(tooltip);
        // TooltipCanvas.Instance.Tooltip = tooltip.GetComponent<RectTransform>();
        tooltipManager.ShowTooltip(Text);
    }

    public void HideTooltip()
    {
        // TooltipCanvas.Instance.Tooltip = null;
        // onTooltipHide?.Invoke();
        // tooltipManager.HideTooltip();
    }
}
