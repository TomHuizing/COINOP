using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    private InputReceiver hoverReceiver; // The receiver that is currently being hovered over
    private Vector2 dragStart;
    private bool isDragging = false;
    private Vector2 previousMousePosition;
    private Vector2 currentMousePosition;

    UnityEvent<Vector2, Vector2> onEndDrag; // Event triggered when the object is dragged
    UnityEvent<Vector2, Vector2> onDrag; // Event triggered when the object is dragged

    private Mouse mouse; // Mouse input handler

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouse = new Mouse(this); // Initialize the mouse input handler
        mouse.OnHover += HandleHover; // Subscribe to the hover event
        mouse.OnClick += HandleClick; // Subscribe to the click event
        mouse.OnDrag += HandleDrag; // Subscribe to the drag event
        mouse.OnScroll += HandleScroll; // Subscribe to the scroll event
        
    }

    // Update is called once per frame
    void Update()
    {
        previousMousePosition = currentMousePosition; // Store the previous mouse position
        currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get the current mouse position
        if (EventSystem.current.IsPointerOverGameObject() && !isDragging) // Check if the pointer is over a UI element
        {
            Unhover(); // Unhover the current object
            return;
        }
        HandleDrag();
        Raycast(); // Perform a raycast to check for input receivers
    }

    private void HandleHover(Vector2 worldPosition, RaycastHit2D[] raycastHits)
    {
        if (raycastHits.Length == 0)
        {
            Unhover(); // Unhover the current object if no raycast hits are found
            return;
        }
        InputReceiver[] receivers = GetInputReceiver(raycastHits); // Get the input receivers from the raycast hits
        if (receivers.Length == 0)
        {
            Unhover(); // Unhover the current object if no input receivers are found
            return;
        }
        if(receivers.Length == 1)
        {
            Hover(receivers[0]); // Hover the first input receiver if only one is found
            return;
        }
    }
    
    private void HandleClick(MouseButton button, Vector2 worldPosition, RaycastHit2D[] raycastHits)
    {
        throw new NotImplementedException();
    }

    private void HandleScroll(float delta)
    {
        throw new NotImplementedException();
    }

    private void HandleDrag(Vector2 start, Vector2 end)
    {
        throw new NotImplementedException();
    }

    private InputReceiver[] GetInputReceiver(RaycastHit2D[] hits)
    {
        InputReceiver[] receivers = hits
            .Where(x => x.collider != null)
            .Select(x => x.collider)
            .Where(x => x.TryGetComponent<InputReceiver>(out _))
            .Select(x => x.GetComponent<InputReceiver>())
            .ToArray();
        if (receivers.Length == 0 || receivers.Length == 1)
            return receivers; // Return empty array if no receivers are found

        InputLayer minLayer = receivers.Min(x => x.Layer); // Get the minimum layer of the receivers
        return receivers
            .Where(x => x.Layer == minLayer) // Filter the receivers to only include those with the minimum layer
            .ToArray();
    }

    private void HandleDrag()
    {
        if (isDragging)
        {
            if (Input.GetMouseButtonUp(0)) // Check if the left mouse button was released
            {
                isDragging = false; // Stop dragging
                onEndDrag?.Invoke(dragStart, (Vector2)Input.mousePosition); // Trigger the end drag event
            }
            else
            {
                onDrag?.Invoke(dragStart, (Vector2)Input.mousePosition); // Trigger the drag event
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0)) // Check if the left mouse button was pressed
            {
                dragStart = currentMousePosition; // Store the starting position of the drag
            }

            if (Input.GetMouseButton(0) && Camera.main.ScreenToWorldPoint(Input.mousePosition) != (Vector3)dragStart) // Check if the left mouse button is being held down
            {
                isDragging = true; // Start dragging
                onDrag?.Invoke(dragStart, Camera.main.ScreenToWorldPoint(Input.mousePosition)); // Trigger the drag event
            }
        }
    }

    private void Raycast()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
        InputReceiver[] receivers = hits
            .Where(x => x.collider != null)
            .Select(x => x.collider)
            .Where(x => x.TryGetComponent<InputReceiver>(out _))
            .Select(x => x.GetComponent<InputReceiver>())
            .ToArray();

        if (receivers.Length == 0)
        {
            HandleMouse(null); // Handle mouse events for no receivers
            return;
        }

        InputLayer minLayer = receivers.Min(x => x.Layer); // Get the minimum layer of the receivers

        receivers = receivers
            .Where(x => x.Layer == minLayer) // Filter the receivers to only include those with the minimum layer
            .ToArray();

        foreach (var receiver in receivers)
        {
            HandleMouse(receiver); // Handle mouse events for the overlapping receivers
        }
        
        //TODO: Handle drag events
    }

    private void HandleMouse(InputReceiver receiver) // Method to handle mouse events
    {
        if(receiver != hoverReceiver) // Check if the ray hit a different object than the last frame
        {
            Unhover(); // Unhover the previous object
            Hover(receiver); // Hover the new object
        }
        if(Input.GetMouseButtonDown(0)) // Check if the left mouse button was clicked
        {
            if(receiver != null) // Check if the ray hit something
                receiver.Click(MouseButton.Left); // Send click event to the object that was hit
        }
        if(Input.GetMouseButtonDown(1)) // Check if the left mouse button was clicked
        {
            if(receiver != null) // Check if the ray hit something
                receiver.Click(MouseButton.Right); // Send click event to the object that was hit
        }
        if(Input.GetMouseButtonDown(2)) // Check if the left mouse button was clicked
        {
            if(receiver != null) // Check if the ray hit something
                receiver.Click(MouseButton.Middle); // Send click event to the object that was hit
        }
    }

    private void Unhover() // Method to unhover the current object
    {
        if(hoverReceiver != null) // Check if there was a previous object
            hoverReceiver.MouseExit(); // Send mouse exit event to the previous object
        hoverReceiver = null; // Reset the current object
    }

    private void Hover(InputReceiver receiver) // Method to handle hover events
    {
        hoverReceiver = receiver; // Update the current object
        if(hoverReceiver != null) // Check if the ray hit something
            hoverReceiver.MouseEnter(); // Send mouse enter event to the new object
    }


    // private void SendClick(Collider2D collider, MouseButton button = MouseButton.Left) // Method to handle click events
    // {
    //     if (collider.TryGetComponent<InputReceiver>(out var receiver))
    //         receiver.Click(button);
    // }

    // private void SendMouseEnter(Collider2D collider) // Method to handle mouse enter events
    // {
    //     if (collider.TryGetComponent<InputReceiver>(out var receiver))
    //         receiver.MouseEnter();
    // }

    // private void SendMouseExit(Collider2D collider) // Method to handle mouse exit events
    // {
    //     if (collider.TryGetComponent<InputReceiver>(out var receiver))
    //         receiver.MouseExit();
    // }
}
