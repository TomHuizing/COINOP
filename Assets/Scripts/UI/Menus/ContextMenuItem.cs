using System;
using TMPro;
using UnityEngine;

public class ContextMenuItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI label;

    public event Action OnClick;

    public void Init(string text, bool enabled = true)
    {
        label.text = text;
        name = text;
    }

    public void Click() => OnClick?.Invoke();
}
