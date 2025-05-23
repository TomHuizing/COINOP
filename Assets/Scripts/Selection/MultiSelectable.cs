using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Selectable))]
public class MultiSelectable : MonoBehaviour
{
    private static readonly List<MultiSelectable> multiSelectables = new(); // List of currently selected objects
    public static IEnumerable<MultiSelectable> MultiSelectables => multiSelectables; // Public property to access the list of selected objects

    [SerializeField] private UnityEvent onMultiSelect = new(); // Event triggered when the object is selected with multiple selection

    public UnityEvent OnMultiSelect => onMultiSelect; // Public property to access the onMultiSelect event

    void OnEnable()
    {
        if (multiSelectables.Contains(this))
            return;
        multiSelectables.Add(this); // Add this object to the list of selected objects
    }

    void OnDisable() => multiSelectables.Remove(this); // Remove this object from the list of selected objects

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
