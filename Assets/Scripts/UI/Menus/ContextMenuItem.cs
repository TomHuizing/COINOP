using System;
using TMPro;
using UnityEngine;

public class ContextMenuItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI label;

    public event Action OnClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(string text)
    {
        label.text = text;
    }

    public void Click() => OnClick?.Invoke();
}
