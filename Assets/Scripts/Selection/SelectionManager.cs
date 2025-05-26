using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private readonly List<Selectable> selected = new();

    public static SelectionManager Instance; // Singleton instance of SelectionManager

    //public TextMeshProUGUI selectionText; // Reference to the TextMeshProUGUI component for displaying selection information

    [SerializeField] private RectTransform selectionUiParent; // Parent transform for the selection UI
    [SerializeField] private SelectionBox selectionBox; // Reference to the SelectionBox component for visualizing selection area

    private GameObject selectionUi;

    public IEnumerable<Selectable> Selected => selected; // Public property to access the currently selected objects

    void Awake()
    {
        if (Instance == null)
            Instance = this; // Set the singleton instance
        else
            Destroy(gameObject); // Destroy duplicate instances
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if the Escape key is pressed
        {
            DeselectAll(); // Deselect all objects
        }
    }

    public void Select(Selectable selectable)
    {
        if (selectable == null || (selected.Contains(selectable) && selected.Count == 1))
            return; // If the selectable is null, do nothing
        foreach (var s in selected.Where(s => s != selectable))
        {
            s.OnDeselected.Invoke(); // Invoke the deselect event for all currently selected objects
        }
        selected.Clear(); // Clear the currently selected objects
        selected.Add(selectable); // Add the new selectable to the selected list
        selectable.OnSelected.Invoke(); // Invoke the select event for the newly selected object
    }
    
    public void SelectRange(IEnumerable<Selectable> selectables)
    {
        if (selectables == null)
            return; // If the selectables collection is null, do nothing

        foreach (var s in selected)
        {
            s.OnDeselected.Invoke(); // Invoke the deselect event for all currently selected objects
        }

        selected.Clear(); // Clear the currently selected objects

        foreach (var selectable in selectables.Where(s => s != null)) // Iterate through the provided selectables
        {
            selectable.OnMultiSelect.Invoke(); // Invoke the select event for each selectable
            selected.Add(selectable); // Add the selectable to the selected list
        }
    }

    public void Add(Selectable selectable)
    {
        if (selectable == null || selected.Contains(selectable))
            return; // If the selectable is null or already selected, do nothing

        selectable.OnMultiSelect.Invoke(); // Invoke the select event for the newly selected object
        selected.Add(selectable); // Add the new selectable to the selected list
    }

    public void AddRange(IEnumerable<Selectable> selectables)
    {
        if (selectables == null)
            return; // If the selectables collection is null, do nothing

        foreach (var s in selected.Where(s => !selectables.Contains(s)))
        {
            s.OnDeselected.Invoke(); // Invoke the deselect event for all currently selected objects
        }

        foreach (var selectable in selectables.Where(s => s != null && !selected.Contains(s))) // Iterate through the provided selectables
        {
            selectable.OnMultiSelect.Invoke(); // Invoke the multi-select event for each selectable
            selected.Add(selectable); // Add the selectable to the selected list
        }
    }

    public void Deselect(Selectable selectable)
    {
        if (selectable == null || !selected.Contains(selectable))
            return; // If the selectable is null or not currently selected, do nothing

        selectable.OnDeselected.Invoke(); // Invoke the deselect event for the specified object
        selected.Remove(selectable); // Remove the selectable from the selected list
    }

    public void DeselectAll()
    {
        foreach (var s in selected)
        {
            s.OnDeselected.Invoke(); // Invoke the deselect event for all currently selected objects
        }
        selected.Clear(); // Deselect the currently selected object
    }

    public void Drag(Vector2 start, Vector2 end)
    {
        if (selectionBox == null)
            return; // If the selection box is not set, do nothing

        selectionBox.SetBox(start, end); // Set the selection box to the specified start and end positions
    }

    public void EndDrag(Vector2 start, Vector2 end)
    {
        if (selectionBox == null)
            return; // If the selection box is not set, do nothing

        selectionBox.Hide(); // Hide the selection box
        var rect = new Rect(start, end - start); // Create a rectangle from the start and end positions
        //var selectables = FindObjectsOfType<Selectable>(); // Find all selectable objects in the scene
        SelectRange(Selectable.GetSelectablesInBox(rect)); // Select the objects within the rectangle
    }
}
