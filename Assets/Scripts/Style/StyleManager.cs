using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class StyleManager : MonoBehaviour
{
    [Serializable]
    public struct MaterialColorbinder
    {
        public PaletteColor ColorName;
        public bool OverrideAlpha;
        [Range(0, 1)] public float Alpha;
        public List<Material> Materials;
    }

    public static StyleManager Instance { get; private set; }

    [SerializeField] private ColorPalette colorPalette;
    [SerializeField] private List<MaterialColorbinder> materialColorbinders = new();

    public ColorPalette ColorPalette => colorPalette;

    private void OnValidate()
    {
        if (Instance != this)
        {
            if (Instance != null)
                Destroy(Instance.gameObject);
            Instance = this;
#if !UNITY_EDITOR
                DontDestroyOnLoad(gameObject);
#endif
        }
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
#if !UNITY_EDITOR
                DontDestroyOnLoad(gameObject);
#endif
        }
    }

    private void Update()
    {
        foreach (var binder in materialColorbinders)
        {
            var color = colorPalette.GetColor(binder.ColorName);
            if (binder.OverrideAlpha)
                color.a = binder.Alpha;
            foreach (var material in binder.Materials)
            {
                if (material != null)
                    material.color = color;
            }
        }
    }
}
