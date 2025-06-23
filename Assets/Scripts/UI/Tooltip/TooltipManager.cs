using System;
using RainbowArt.CleanFlatUI;
using UnityEngine;

[CreateAssetMenu(fileName = "TooltipManager", menuName = "Singletons/TooltipManager")]
public class TooltipManager : ScriptableObject
{
    [SerializeField] float delay;
    [SerializeField] Canvas canvas;
    [SerializeField] TooltipSpecial tooltip;

    public void ShowTooltip(string message)
    {
        // Canvas tooltipCanvas = canvas ?? TooltipCanvas.Instance.GetComponent<Canvas>();
        // tooltip.DescriptionValue = message;
        // tooltip.ShowTooltip(TimeSpan.FromSeconds(delay));
    }

    public void HideTooltip()
    {
        tooltip.HideTooltip();
    }
}
