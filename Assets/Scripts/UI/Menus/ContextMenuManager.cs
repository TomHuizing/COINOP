using System;
using System.Collections.Generic;
using System.Linq;
using InputSystem;
using UnityEngine;

public class ContextMenuManager : MonoBehaviour
{
    public static ContextMenuManager Instance;


    [SerializeField] RainbowArt.CleanFlatUI.ContextMenu contextMenu;

    // private Mouse mouse;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //mouse = new(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowContextMenu(Dictionary<string,Action> items)
    {
        // contextMenu.Show(mouse.ScreenPosition, items.Keys.ToArray(), i => items.Values.ToArray()[i]?.Invoke());
    }
}
