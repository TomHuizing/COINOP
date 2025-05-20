using System;
using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    private static SelectionManager Manager => SelectionManager.Instance != null ? SelectionManager.Instance : throw new NullReferenceException("SelectionManager instance not found"); // Reference to the SelectionManager instance

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

    public bool IsSelected => SelectionManager.Instance.Selected == this; // Check if this object is currently selected

    public void Select() => Manager.Selected = this; // Set this object as the selected object

    public void ContextClick()
    {
        if (Manager.Selected != null)
            Manager.Selected.OnContext?.Invoke(this);
    }

    public GameObject GetSelectedUi()
    {
        if (selectedUiPrefab == null)
            return null;

        GameObject selectedUi = Instantiate(selectedUiPrefab, Vector3.zero, Quaternion.identity); // Instantiate the selected UI prefab
        OnInitSelectedUi?.Invoke(selectedUi);
        return selectedUi;
    }
}


