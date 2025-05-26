using System;
using System.Collections.Generic;
using System.Linq;
using InputSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    private static SelectionManager Manager => SelectionManager.Instance != null ? SelectionManager.Instance : throw new NullReferenceException("SelectionManager instance not found"); // Reference to the SelectionManager instance
    private static readonly List<Selectable> selectables = new(); // List of currently selected objects
    public static IEnumerable<Selectable> Selectables => selectables; // Public property to access the list of selected objects

    [SerializeField] private bool isMultiSelectable = false; // Flag to indicate if this object can be selected with multiple selection

    [SerializeField] private UnityEvent onSelected = new(); // Event triggered when the object is selected
    [SerializeField] private UnityEvent onMultiSelect = new(); // Event triggered when the object is selected with multiple selection
    [SerializeField] private UnityEvent onDeselected = new(); // Event triggered when the object is deselected
    [SerializeField] private UnityEvent<Selectable> onContext = new();
    [SerializeField] private UnityEvent onHover = new(); // Event triggered when the object is hovered over
    [SerializeField] private UnityEvent onUnhover = new(); // Event triggered when the object is unhovered
    [SerializeField] private UnityEvent<GameObject> onInitSelectedUi = new(); // Event triggered when the selected UI is initialized

    [SerializeField] private GameObject selectedUiPrefab; // UI element to show when the object is selected
    [SerializeField] private GameObject multiSelectedUiPrefab; // UI element to show when the object is selected with multiple selection

    public UnityEvent OnSelected => onSelected; // Public property to access the onSelected event
    public UnityEvent OnMultiSelect => onMultiSelect; // Public property to access the onMultiSelect event
    public UnityEvent OnDeselected => onDeselected; // Public property to access the onDeselected event
    public UnityEvent<Selectable> OnContext => onContext; // Public property to access the onContext event
    public UnityEvent OnHover => onHover; // Public property to access the onHover event
    public UnityEvent OnUnhover => onUnhover; // Public property to access the onUnhover event

    public UnityEvent<GameObject> OnInitSelectedUi => onInitSelectedUi; // Public property to access the onInitSelectedUi event
    public UnityEvent<GameObject> OnInitMultiSelectedUi => onInitSelectedUi; // Public property to access the onInitSelectedUi event

    public bool IsMultiSelectable => isMultiSelectable; // Public property to access the isMultiSelectable flag
    public bool IsSelected => SelectionManager.Instance.Selected.Contains(this); // Check if this object is currently selected

    //public void Select() => Manager.Selected = this; // Set this object as the selected object

    static public IEnumerable<Selectable> GetSelectablesInBox(Rect selectionBox) => selectables.Where(s => s != null && s.gameObject.activeInHierarchy && selectionBox.Contains(s.transform.position)); // Return all selectables within the given selection box

    public void OnDisable() => Manager.Deselect(this); // Deselect this object when it is disabled

    public void ContextClick(KeyModifiers modifiers)
    {
        foreach (Selectable selectable in Manager.Selected)
        {
            if (selectable == null || !selectable.gameObject.activeInHierarchy)
                continue; // Skip if the selectable is null or inactive
            selectable.OnContext.Invoke(this); // Invoke the context event for each selected object
        }
    }

    public void PrimaryClick(KeyModifiers modifiers)
    {
        if (!IsSelected)
        {
            switch (modifiers)
            {
                case KeyModifiers.None:
                    Manager.Select(this); // Select this object if no modifiers are pressed
                    break;
                case KeyModifiers.Shift:
                    if (IsMultiSelectable)
                        Manager.Add(this); // Select this object with multiple selection
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
    
    public GameObject GetMultiSelectedUi()
    {
        if (multiSelectedUiPrefab == null)
            return null;

        GameObject multiSelectedUi = Instantiate(multiSelectedUiPrefab, Vector3.zero, Quaternion.identity); // Instantiate the multi-selected UI prefab
        OnInitSelectedUi?.Invoke(multiSelectedUi);
        return multiSelectedUi;
    }
}


