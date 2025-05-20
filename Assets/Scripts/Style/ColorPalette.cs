using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Scriptable Objects/ColorPalette")]
public class ColorPalette : ScriptableObject
{
    // public List<PaletteColor> colors;

    [Header("Text Colors")]
    [SerializeField] Color textColor;
    [SerializeField] Color highlightColor;
    [SerializeField] Color subduedColor;
    [SerializeField] Color warningColor;
    [SerializeField] Color dangerColor;

    [Header("Panel Colors")]
    [SerializeField] Color backgroundColor;
    [SerializeField] Color backgroundAltColor;
    [SerializeField] Color borderColor;

    [Header("Button Colors")]
    [SerializeField] Color buttonColor;
    [SerializeField] Color buttonHoverColor;
    [SerializeField] Color buttonPressedColor;
    [SerializeField] Color buttonDisabledColor;

    [Header("Common Colors")]
    [SerializeField] Color blue;
    [SerializeField] Color green;
    [SerializeField] Color yellow;
    [SerializeField] Color red;

    [Header("Map Colors")]
    [SerializeField] Color water;
    [SerializeField] Color residential;
    [SerializeField] Color commercial;
    [SerializeField] Color industrial;
    [SerializeField] Color greenSpace;
    [SerializeField] Color majorRoad;
    [SerializeField] Color minorRoad;
    [SerializeField] Color mapBackground;

    public Color TextColor => textColor;
    public Color HighlightColor => highlightColor;
    public Color SubduedColor => subduedColor;
    public Color WarningColor => warningColor;
    public Color DangerColor => dangerColor;
    public Color BackgroundColor => backgroundColor;
    public Color BackgroundAltColor => backgroundAltColor;
    public Color BorderColor => borderColor;
    public Color ButtonColor => buttonColor;
    public Color ButtonHoverColor => buttonHoverColor;
    public Color ButtonPressedColor => buttonPressedColor;
    public Color ButtonDisabledColor => buttonDisabledColor;
    public Color Blue => blue;
    public Color Green => green;
    public Color Yellow => yellow;
    public Color Red => red;

    public Color GetColor(PaletteColor color)
    {
        return color switch
        {
            PaletteColor.TextColor => textColor,
            PaletteColor.HighlightColor => highlightColor,
            PaletteColor.SubduedColor => subduedColor,
            PaletteColor.WarningColor => warningColor,
            PaletteColor.DangerColor => dangerColor,
            PaletteColor.BackgroundColor => backgroundColor,
            PaletteColor.BackgroundAltColor => backgroundAltColor,
            PaletteColor.BorderColor => borderColor,
            PaletteColor.ButtonColor => buttonColor,
            PaletteColor.ButtonHoverColor => buttonHoverColor,
            PaletteColor.ButtonPressedColor => buttonPressedColor,
            PaletteColor.ButtonDisabledColor => buttonDisabledColor,
            PaletteColor.Blue => blue,
            PaletteColor.Green => green,
            PaletteColor.Yellow => yellow,
            PaletteColor.Red => red,
            _ => Color.magenta
        };
    }
}
