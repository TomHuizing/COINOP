using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Scriptable Objects/ColorPalette")]
public class ColorPalette : ScriptableObject
{
    // public List<PaletteColor> colors;

    [Header("Text Colors Dark")]
    [SerializeField] Color textColorDark;
    [SerializeField] Color highlightColorDark;
    [SerializeField] Color subduedColorDark;
    [SerializeField] Color warningColorDark;
    [SerializeField] Color dangerColorDark;

    [Header("Text Colors Dark")]
    [SerializeField] Color textColorLight;
    [SerializeField] Color highlightColorLight;
    [SerializeField] Color subduedColorLight;
    [SerializeField] Color warningColorLight;
    [SerializeField] Color dangerColorLight;

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

    public Color TextColorDark => textColorDark;
    public Color HighlightColorDark => highlightColorDark;
    public Color SubduedColorDark => subduedColorDark;
    public Color WarningColorDark => warningColorDark;
    public Color DangerColorDark => dangerColorDark;
    public Color TextColorLight => textColorLight;
    public Color HighlightColorLight => highlightColorLight;
    public Color SubduedColorLight => subduedColorLight;
    public Color WarningColorLight => warningColorLight;
    public Color DangerColorLight => dangerColorLight;
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
            PaletteColor.TextColorDark => textColorDark,
            PaletteColor.HighlightColorDark => highlightColorDark,
            PaletteColor.SubduedColorDark => subduedColorDark,
            PaletteColor.WarningColorDark => warningColorDark,
            PaletteColor.DangerColorDark => dangerColorDark,
            PaletteColor.TextColorLight => textColorLight,
            PaletteColor.HighlightColorLight => highlightColorLight,
            PaletteColor.SubduedColorLight => subduedColorLight,
            PaletteColor.WarningColorLight => warningColorLight,
            PaletteColor.DangerColorLight => dangerColorLight,
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
