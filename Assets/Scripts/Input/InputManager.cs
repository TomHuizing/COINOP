using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace InputSystem
{
    public class InputManager : MonoBehaviour
    {
        private InputReceiver currenHoverReceiver; // The receiver that is currently being hovered over
                                                   // private Vector2 dragStart;
                                                   // private bool isDragging = false;
                                                   // private Vector2 previousMousePosition;
                                                   // private Vector2 currentMousePosition;

        // readonly UnityEvent<Vector2, Vector2> onEndDrag; // Event triggered when the object is dragged
        // readonly UnityEvent<Vector2, Vector2> onDrag; // Event triggered when the object is dragged
        [SerializeField] private UnityEvent<Vector2, Vector2> onDrag; // Event triggered when the object is dragged
        [SerializeField] private UnityEvent<Vector2, Vector2> onEndDrag; // Event triggered when the object is dragged

        // private Mouse mouse; // Mouse input handler

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // mouse = new(this);
            // mouse.OnHover += HandleHover; // Subscribe to the hover event
            // mouse.OnClick += HandleClick; // Subscribe to the click event
            // mouse.OnDrag += HandleDrag; // Subscribe to the drag event
            // mouse.OnEndDrag += HandleEndDrag; // Subscribe to the end drag event
            // mouse.OnScroll += HandleScroll; // Subscribe to the scroll event
            // mouse.OnDrag += (start, end) => onDrag?.Invoke(start, end); // Trigger the drag event
            // mouse.OnEndDrag += (start, end) => onEndDrag?.Invoke(start, end); // Trigger the end drag event
        
        }

        // Update is called once per frame
        void Update()
        {
            // previousMousePosition = currentMousePosition; // Store the previous mouse position
            // currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get the current mouse position
            // if (EventSystem.current.IsPointerOverGameObject() && !isDragging) // Check if the pointer is over a UI element
            // {
            //     Unhover(); // Unhover the current object
            //     return;
            // }
            // HandleDrag();
            // Raycast(); // Perform a raycast to check for input receivers
        }

        private void HandleHover(Vector2 worldPosition, RaycastHit2D[] raycastHits)
        {
            //print("Hover");
            InputReceiver[] receivers = GetInputReceiver(raycastHits); // Get the input receivers from the raycast hits
            if (receivers.Length == 0)
            {
                Unhover(); // Unhover the current object if no input receivers are found
                return;
            }
            InputReceiver newHoverReceiver = receivers[0]; // Get the receiver with the minimum layer
            if (newHoverReceiver == currenHoverReceiver)
                return; // If the new hover receiver is the same as the current one, do nothing
            Unhover(); // Unhover the current object
            Hover(newHoverReceiver); // Hover the new receiver
        }

        private void HandleClick(MouseButton button, Vector2 worldPosition, RaycastHit2D[] raycastHits)
        {
            var receivers = GetInputReceiver(raycastHits); // Get the input receivers from the raycast hits
            if (receivers.Length > 0)
            {
                receivers[0].Click(button, GetKeyModifiers()); // Send the click event to the first receiver
            }
        }

        private void HandleScroll(float delta)
        {
            
        }

        private void HandleDrag(Vector2 start, Vector2 end)
        {
            onDrag?.Invoke(start, end); // Trigger the drag event with the start and end positions
        }
        
        private void HandleEndDrag(Vector2 start, Vector2 end)
        {
            onEndDrag?.Invoke(start, end); // Trigger the end drag event with the start and end positions
        }

        private InputReceiver[] GetInputReceiver(RaycastHit2D[] hits)
        {
            if (hits == null || hits.Length == 0)
                return Array.Empty<InputReceiver>(); // Return empty array if no hits are found
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
        
        public KeyModifiers GetKeyModifiers()
        {
            KeyModifiers modifiers = KeyModifiers.None; // Initialize the modifiers to none
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                modifiers |= KeyModifiers.Shift; // Add shift modifier if left or right shift is pressed
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                modifiers |= KeyModifiers.Control; // Add control modifier if left or right control is pressed
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                modifiers |= KeyModifiers.Alt; // Add alt modifier if left or right alt is pressed
            if (Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand))
                modifiers |= KeyModifiers.Command; // Add command modifier if left or right command is pressed
            if (Input.GetKey(KeyCode.CapsLock))
                modifiers |= KeyModifiers.CapsLock; // Add caps lock modifier if caps lock is pressed
            if (Input.GetKey(KeyCode.Numlock))
                modifiers |= KeyModifiers.NumLock; // Add num lock modifier if num lock is pressed
            return modifiers; // Return the modifiers
        }

        // private void HandleDrag()
        // {
        //     if (isDragging)
        //     {
        //         if (Input.GetMouseButtonUp(0)) // Check if the left mouse button was released
        //         {
        //             isDragging = false; // Stop dragging
        //             onEndDrag?.Invoke(dragStart, (Vector2)Input.mousePosition); // Trigger the end drag event
        //         }
        //         else
        //         {
        //             onDrag?.Invoke(dragStart, (Vector2)Input.mousePosition); // Trigger the drag event
        //         }
        //     }
        //     else
        //     {
        //         if (Input.GetMouseButtonDown(0)) // Check if the left mouse button was pressed
        //         {
        //             dragStart = currentMousePosition; // Store the starting position of the drag
        //         }

        //         if (Input.GetMouseButton(0) && Camera.main.ScreenToWorldPoint(Input.mousePosition) != (Vector3)dragStart) // Check if the left mouse button is being held down
        //         {
        //             isDragging = true; // Start dragging
        //             onDrag?.Invoke(dragStart, Camera.main.ScreenToWorldPoint(Input.mousePosition)); // Trigger the drag event
        //         }
        //     }
        // }

        // private void Raycast()
        // {
        //     RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
        //     InputReceiver[] receivers = hits
        //         .Where(x => x.collider != null)
        //         .Select(x => x.collider)
        //         .Where(x => x.TryGetComponent<InputReceiver>(out _))
        //         .Select(x => x.GetComponent<InputReceiver>())
        //         .ToArray();

        //     if (receivers.Length == 0)
        //     {
        //         HandleMouse(null); // Handle mouse events for no receivers
        //         return;
        //     }

        //     InputLayer minLayer = receivers.Min(x => x.Layer); // Get the minimum layer of the receivers

        //     receivers = receivers
        //         .Where(x => x.Layer == minLayer) // Filter the receivers to only include those with the minimum layer
        //         .ToArray();

        //     foreach (var receiver in receivers)
        //     {
        //         HandleMouse(receiver); // Handle mouse events for the overlapping receivers
        //     }

        //     //TODO: Handle drag events
        // }

        // private void HandleMouse(InputReceiver receiver) // Method to handle mouse events
        // {
        //     if(receiver != hoverReceiver) // Check if the ray hit a different object than the last frame
        //     {
        //         Unhover(); // Unhover the previous object
        //         Hover(receiver); // Hover the new object
        //     }
        //     if(Input.GetMouseButtonDown(0)) // Check if the left mouse button was clicked
        //     {
        //         if(receiver != null) // Check if the ray hit something
        //             receiver.Click(MouseButton.Left); // Send click event to the object that was hit
        //     }
        //     if(Input.GetMouseButtonDown(1)) // Check if the left mouse button was clicked
        //     {
        //         if(receiver != null) // Check if the ray hit something
        //             receiver.Click(MouseButton.Right); // Send click event to the object that was hit
        //     }
        //     if(Input.GetMouseButtonDown(2)) // Check if the left mouse button was clicked
        //     {
        //         if(receiver != null) // Check if the ray hit something
        //             receiver.Click(MouseButton.Middle); // Send click event to the object that was hit
        //     }
        // }

        private void Unhover() // Method to unhover the current object
        {
            if (currenHoverReceiver != null) // Check if there was a previous object
                currenHoverReceiver.MouseExit(); // Send mouse exit event to the previous object
            currenHoverReceiver = null; // Reset the current object
        }

        private void Hover(InputReceiver receiver) // Method to handle hover events
        {
            currenHoverReceiver = receiver; // Update the current object
            if (currenHoverReceiver != null) // Check if the ray hit something
                currenHoverReceiver.MouseEnter(); // Send mouse enter event to the new object
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
}
