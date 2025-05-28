using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour
{
    private readonly List<Selectable> selected = new();

    public static SelectionManager Instance; // Singleton instance of SelectionManager

    //public TextMeshProUGUI selectionText; // Reference to the TextMeshProUGUI component for displaying selection information

    [SerializeField] private RectTransform selectionUiParent; // Parent transform for the selection UI
    [SerializeField] private SelectionBox selectionBox; // Reference to the SelectionBox component for visualizing selection area
    [SerializeField] private UnityEvent<Selectable[]> OnSelectionChange;

    private readonly List<GameObject> selectionUi = new();

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
                                        // if (selectionUi != null)
                                        //     Destroy(selectionUi); // Destroy the previous selection UI if it exists
                                        // selectionUi = selectable.GetSelectedUi(); // Instantiate the new selection UI
                                        // selectionUi.transform.SetParent(selectionUiParent, false); // Set the parent of the selection UI
                                        // UpdateSelectionUI(); // Update the selection UI to reflect the new selection
        OnSelectionChange?.Invoke(selected.ToArray());
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
        // UpdateSelectionUI(); // Update the selection UI to reflect the new selection
        OnSelectionChange?.Invoke(selected.ToArray());
    }

    public void Add(Selectable selectable)
    {
        if (selectable == null || selected.Contains(selectable))
            return; // If the selectable is null or already selected, do nothing

        selectable.OnMultiSelect.Invoke(); // Invoke the select event for the newly selected object
        selected.Add(selectable); // Add the new selectable to the selected list
                                  // UpdateSelectionUI(); // Update the selection UI to reflect the new selection
        OnSelectionChange?.Invoke(selected.ToArray());
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
        // UpdateSelectionUI(); // Update the selection UI to reflect the new selection
        OnSelectionChange?.Invoke(selected.ToArray());
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
        // UpdateSelectionUI(); // Update the selection UI to reflect the deselection
        OnSelectionChange?.Invoke(selected.ToArray());
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
        Rect rect = new(Vector2.Min(start, end), Vector2.Max(start, end) - Vector2.Min(start, end)); // Create a rectangle from the start and end positions
        IEnumerable<Selectable> s = Selectable.GetSelectablesInBox(rect).Where(s => s.IsMultiSelectable);
        SelectRange(s); // Select the objects within the rectangle
    }

    // public void UpdateSelectionUI()
    // {
    //     foreach (var s in selectionUi)
    //     {
    //         if (s != null)
    //             Destroy(s);
    //     }
    //     selectionUi.Clear(); // Clear the previous selection UI
    //     if (selected.Count == 0)
    //         return; // If no objects are selected, do nothing
    //     if (selected.Count == 1)
    //         selectionUi.Add(selected.First().GetSelectedUi()); // Get the UI for the single selected object
    //     else
    //         selectionUi.AddRange(selected.Select(s => s.GetMultiSelectedUi())); // Get the UI for multiple selected objects
    //     int offset = 0;
    //     foreach (var ui in selectionUi)
    //     {
    //         if (ui != null)
    //         {
    //             ui.transform.SetParent(selectionUiParent, false); // Set the parent of the selection UI
    //             ui.transform.localPosition = new Vector3(offset, 0, 0); // Position the UI vertically
    //             ui.SetActive(true); // Activate the selection UI
    //             offset += 25;
    //         }
    //     }
    // }
}
