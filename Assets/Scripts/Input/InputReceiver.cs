using System;
using InputSystem;
using UnityEngine;
using UnityEngine.Events;

namespace InputSystem
{
    [Serializable]
    public class InputReceiver : MonoBehaviour
    {
        // [SerializeField] private float keyRepeatDelay = 0.5f;
        // [SerializeField] private float keyRepeatSpeed = 0.1f;
        [SerializeField] private InputLayer layer = InputLayer.Map;

        [SerializeField] private UnityEvent<MouseButton, KeyModifiers> onClick = new(); // Event triggered when the object is clicked
        [SerializeField] private UnityEvent<KeyModifiers> onPrimaryClick = new(); // Event triggered when the object is clicked
        [SerializeField] private UnityEvent<KeyModifiers> onSecondaryClick = new(); // Event triggered when the object is clicked
        [SerializeField] private UnityEvent<KeyModifiers> onTertiaryClick = new(); // Event triggered when the object is clicked
        [SerializeField] private UnityEvent onMouseEnter = new(); // Event triggered when the mouse is over the object
        [SerializeField] private UnityEvent onMouseExit = new(); // Event triggered when the mouse exits the object

        [SerializeField] private UnityEvent onEscape = new(); // Event triggered when the escape key is pressed
        [SerializeField] private UnityEvent onEnter = new(); // Event triggered when the enter key is pressed
        [SerializeField] private UnityEvent onSpace = new(); // Event triggered when the space key is pressed

        [SerializeField] private UnityEvent<KeyCode> onKeyDown = new(); // Event triggered when a key is pressed
        [SerializeField] private UnityEvent<KeyCode> onKeyUp = new(); // Event triggered when a key is released
        //[SerializeField] private UnityEvent<KeyCode> onKeyInput = new(); // Event triggered when a key is pressed and held

        public InputLayer Layer => layer; // Public property to access the input layer

        public UnityEvent<MouseButton, KeyModifiers> OnClick => onClick; // Public property to access the onClick event
        public UnityEvent<KeyModifiers> OnPrimaryClick => onPrimaryClick; // Public property to access the onPrimaryClick event
        public UnityEvent<KeyModifiers> OnSecondaryClick => onSecondaryClick; // Public property to access the onSecondaryClick event
        public UnityEvent<KeyModifiers> OnTertiaryClick => onTertiaryClick; // Public property to access the onTertiaryClick event
        public UnityEvent OnMouseEnter => onMouseEnter; // Public property to access the onMouseEnter event
        public UnityEvent OnMouseExit => onMouseExit; // Public property to access the onMouseExit event

        public void Click(MouseButton button, KeyModifiers modifiers) // Method to handle click events
        {
            onClick?.Invoke(button, modifiers); // Invoke the click event if it is not null
            switch (button) // Check which button was clicked
            {
                case MouseButton.Left:
                    onPrimaryClick?.Invoke(modifiers); // Invoke the primary click event if it is not null
                    break;
                case MouseButton.Right:
                    onSecondaryClick?.Invoke(modifiers); // Invoke the secondary click event if it is not null
                    break;
                case MouseButton.Middle:
                    onTertiaryClick?.Invoke(modifiers); // Invoke the tertiary click event if it is not null
                    break;
            }
        }

        public void MouseEnter()
        {
            onMouseEnter?.Invoke(); // Invoke the mouse enter event if it is not null
        }
        public void MouseExit()
        {
            onMouseExit?.Invoke(); // Invoke the mouse exit event if it is not null
        }

        public void KeyDown(KeyCode key) // Method to handle key press events
        {
            switch (key) // Check which key was pressed
            {
                case KeyCode.Escape:
                    onEscape?.Invoke(); // Invoke the escape event if it is not null
                    break;
                case KeyCode.Return:
                    onEnter?.Invoke(); // Invoke the enter event if it is not null
                    break;
                case KeyCode.Space:
                    onSpace?.Invoke(); // Invoke the space event if it is not null
                    break;
            }
            onKeyDown?.Invoke(key); // Invoke the key down event if it is not null
        }

        public void KeyUp(KeyCode key) // Method to handle key release events
        {
            onKeyUp?.Invoke(key); // Invoke the key up event if it is not null
        }
    }
}
