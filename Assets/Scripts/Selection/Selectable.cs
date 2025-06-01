using System;
using System.Collections.Generic;
using System.Linq;
using InputSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    //private static SelectionManager Manager => SelectionManager.Instance; // Reference to the SelectionManager instance
    private static readonly List<Selectable> selectables = new(); // List of currently selected objects
    public static IEnumerable<Selectable> Selectables => selectables; // Public property to access the list of selected objects

    [SerializeField] SelectionManager selectionManager;
    [SerializeField] private bool isMultiSelectable = false; // Flag to indicate if this object can be selected with multiple selection
    [SerializeField] private SelectionLayer layer;

    [SerializeField] private UnityEvent onSelected = new(); // Event triggered when the object is selected
    [SerializeField] private UnityEvent onDeselected = new(); // Event triggered when the object is deselected
    [SerializeField] private UnityEvent<Selectable> onContext = new();
    [SerializeField] private UnityEvent<Selectable> onAddContext = new();
    [SerializeField] private UnityEvent<Selectable> onMenu = new();
    [SerializeField] private UnityEvent onHover = new(); // Event triggered when the object is hovered over
    [SerializeField] private UnityEvent onUnhover = new(); // Event triggered when the object is unhovered
    [SerializeField] private UnityEvent<GameObject> onInitSelectedUi = new(); // Event triggered when the selected UI is initialized
    [SerializeField] private UnityEvent<GameObject> onInitMultiSelectedUi = new(); // Event triggered when the selected UI is initialized

    [SerializeField] private GameObject selectedUiPrefab; // UI element to show when the object is selected

    public UnityEvent OnSelected => onSelected; // Public property to access the onSelected event
    public UnityEvent OnDeselected => onDeselected; // Public property to access the onDeselected event
    public UnityEvent<Selectable> OnContext => onContext; // Public property to access the onContext event
    public UnityEvent<Selectable> OnAddContext => onAddContext; // Public property to access the onContext event
    public UnityEvent<Selectable> OnMenu => onMenu; // Public property to access the onContext event
    public UnityEvent OnHover => onHover; // Public property to access the onHover event
    public UnityEvent OnUnhover => onUnhover; // Public property to access the onUnhover event

    public UnityEvent<GameObject> OnInitSelectedUi => onInitSelectedUi; // Public property to access the onInitSelectedUi event
    public UnityEvent<GameObject> OnInitMultiSelectedUi => onInitMultiSelectedUi; // Public property to access the onInitSelectedUi event

    public bool IsMultiSelectable => isMultiSelectable; // Public property to access the isMultiSelectable flag
    public SelectionLayer Layer => layer;
    public bool IsSelected => selectionManager.Selected.Contains(this); // Check if this object is currently selected

    //public void Select() => Manager.Selected = this; // Set this object as the selected object

    static public IEnumerable<Selectable> GetSelectablesInBox(Rect selectionBox)
    {
        return selectables.Where(s => s != null && selectionBox.Contains(s.transform.position)); // Return all selectables within the given selection box
    }

    void Start()
    {
        selectables.Add(this); // Add this object to the list of selectables when it starts
    }

    public void OnDisable()
    {
        if (IsSelected && selectionManager != null)
            selectionManager.Deselect(this); // Deselect this object when it is disabled
    }

    public void ContextClick(KeyModifiers modifiers)
    {
        foreach (Selectable selectable in selectionManager.Selected.Where(s => s != null && s.gameObject.activeInHierarchy))
        {
            switch (modifiers)
            {
                case KeyModifiers.None:
                    selectable.OnContext.Invoke(this);
                    break;
                case KeyModifiers.Shift:
                    selectable.OnAddContext.Invoke(this);
                    break;
                case KeyModifiers.Control:
                    selectable.OnMenu.Invoke(this);
                    break;
                
            }
        }
    }

    public void PrimaryClick(KeyModifiers modifiers)
    {
        if (!IsSelected)
        {
            switch (modifiers)
            {
                case KeyModifiers.None:
                    selectionManager.Select(this); // Select this object if no modifiers are pressed
                    break;
                case KeyModifiers.Shift:
                    if (IsMultiSelectable)
                        selectionManager.AddToSelection(this); // Select this object with multiple selection
                    break;
            }
        }
    }

    public GameObject GetSelectedUi()
    {
        if (selectedUiPrefab == null)
            return null;

        GameObject selectedUi = Instantiate(selectedUiPrefab, Vector3.zero, Quaternion.identity); // Instantiate the selected UI prefab
        OnInitSelectedUi?.Invoke(selectedUi);
        return selectedUi;
    }
    
    // public GameObject GetMultiSelectedUi()
    // {
    //     if (multiSelectedUiPrefab == null)
    //         return null;

    //     GameObject multiSelectedUi = Instantiate(multiSelectedUiPrefab, Vector3.zero, Quaternion.identity); // Instantiate the multi-selected UI prefab
    //     OnInitMultiSelectedUi?.Invoke(multiSelectedUi);
    //     return multiSelectedUi;
    // }
}


