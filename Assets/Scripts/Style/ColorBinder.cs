using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ColorBinder : MonoBehaviour
{
    [Serializable]
    public struct ColorBinderData
    {
        public PaletteColor ColorName;
        public bool OverrideAlpha;
        [Range(0, 1)] public float Alpha;
        public List<Renderer> Renderers;
        public List<Graphic> Graphics;
    }

    [SerializeField] private List<Button> buttons = new();
    [SerializeField] private List<ColorBinderData> bindings = new();

    void Update()
    {
        ColorBlock c = new()
        {
            normalColor = StyleManager.Instance.ColorPalette.GetColor(PaletteColor.ButtonColor),
            highlightedColor = StyleManager.Instance.ColorPalette.GetColor(PaletteColor.ButtonHoverColor),
            selectedColor = StyleManager.Instance.ColorPalette.GetColor(PaletteColor.ButtonColor),
            disabledColor = StyleManager.Instance.ColorPalette.GetColor(PaletteColor.ButtonDisabledColor),
            pressedColor = StyleManager.Instance.ColorPalette.GetColor(PaletteColor.ButtonPressedColor),
            colorMultiplier = 1,
        };
        foreach (var button in buttons)
        {
            c.fadeDuration = button.colors.fadeDuration;
            c.colorMultiplier = button.colors.colorMultiplier;
            button.colors = c;
        }
        foreach (var binding in bindings)
            {
                var color = StyleManager.Instance.ColorPalette.GetColor(binding.ColorName);
                if (binding.OverrideAlpha)
                    color.a = binding.Alpha;
                foreach (var renderer in binding.Renderers)
                {
                    if (renderer != null)
                    {
                        // if (renderer is LineRenderer lineRenderer)
                        // {
                        //     lineRenderer.startColor = color;
                        //     lineRenderer.endColor = color;
                        // }
                        // else
                        renderer.sharedMaterial.color = color;
                    }
                }
                foreach (var graphic in binding.Graphics)
                {
                    if (graphic != null)
                        graphic.color = color;
                }
            }
    }
}
