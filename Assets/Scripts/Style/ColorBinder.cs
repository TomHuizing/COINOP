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

    [SerializeField] private List<ColorBinderData> bindings = new();

    void Update()
    {
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
