using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private Selectable selected;

    public static SelectionManager Instance; // Singleton instance of SelectionManager

    //public TextMeshProUGUI selectionText; // Reference to the TextMeshProUGUI component for displaying selection information

    [SerializeField] private RectTransform selectionUiParent; // Parent transform for the selection UI

    private GameObject selectionUi;

    public Selectable Selected
    {
        get => selected;
        set
        {
            if(selected == value)
                return; // If the selected object is the same, do nothing
            if(selected != null)
                selected.OnDeselected.Invoke(); // Invoke the deselect event for the previously selected object
            selected = value; // Set the selected object
            if (selectionUi != null)
                Destroy(selectionUi); // Destroy the previous selection UI if it exists
            if (selected != null)
            {
                selected.OnSelected.Invoke(); // Invoke the select event for the newly selected object
                selectionUi = selected.GetSelectedUi();
                selectionUi.transform.SetParent(selectionUiParent, false); // Set the parent of the selection UI to the specified parent
            }
        }
    } // Currently selected object

    void Awake()
    {
        if (Instance == null)
            Instance = this; // Set the singleton instance
        else
            Destroy(gameObject); // Destroy duplicate instances
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // Check if the Escape key is pressed
        {
            DeselectAll(); // Deselect all objects
        }
    }

    public void DeselectAll()
    {
        Selected = null; // Deselect the currently selected object
    }
}
