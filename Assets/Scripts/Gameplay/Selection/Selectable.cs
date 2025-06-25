using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Selection
{
    public class Selectable : MonoBehaviour
    {
        private static readonly List<Selectable> selectables = new(); // List of currently selected objects
        public static IEnumerable<Selectable> Selectables => selectables; // Public property to access the list of selected objects

        [SerializeField] SelectionManager selectionManager;
        [SerializeField] private bool isMultiSelectable = false; // Flag to indicate if this object can be selected with multiple selection
        [SerializeField] private SelectionLayer layer;

        [SerializeField] private UnityEvent onSelected = new(); // Event triggered when the object is selected
        [SerializeField] private UnityEvent onDeselected = new(); // Event triggered when the object is deselected
        [SerializeField] private UnityEvent<Selectable> onContextClick = new();
        [SerializeField] private UnityEvent<Selectable> onContextAlternate = new();
        [SerializeField] private UnityEvent<Selectable> onContextMenu = new();
        [SerializeField] private UnityEvent onHover = new(); // Event triggered when the object is hovered over
        [SerializeField] private UnityEvent onUnhover = new(); // Event triggered when the object is unhovered

        public UnityEvent OnSelected => onSelected; // Public property to access the onSelected event
        public UnityEvent OnDeselected => onDeselected; // Public property to access the onDeselected event
        public UnityEvent<Selectable> OnContextClick => onContextClick; // Public property to access the onContext event
        public UnityEvent<Selectable> OnContextAlternate => onContextAlternate; // Public property to access the onContext event
        public UnityEvent<Selectable> OnContextMenu => onContextMenu; // Public property to access the onContext event
        public UnityEvent OnHover => onHover; // Public property to access the onHover event
        public UnityEvent OnUnhover => onUnhover; // Public property to access the onUnhover event

        public bool IsMultiSelectable => isMultiSelectable; // Public property to access the isMultiSelectable flag
        public SelectionLayer Layer => layer;
        public bool IsSelected => selectionManager.Selected.Contains(this); // Check if this object is currently selected

        //public void Select() => Manager.Selected = this; // Set this object as the selected object

        static public IEnumerable<Selectable> GetSelectablesInBox(Rect selectionBox)
            => selectables.Where(s => s != null && selectionBox.Contains(s.transform.position));

        void OnEnable()
        {
            selectables.Add(this); // Add this object to the list of selected objects when it is enabled
        }

        void OnDisable()
        {
            if (IsSelected)
                selectionManager.Deselect(this); // Deselect this object when it is disabled
            selectables.Remove(this); // Remove this object from the list of selected objects when it is disabled
        }

        public void ContextClick(Selectable selectable) => onContextClick.Invoke(selectable); // Invoke the context click event
        public void ContextMenu(Selectable selectable) => onContextMenu.Invoke(selectable); // Invoke the context menu event
        public void ContextAlternate(Selectable selectable) => onContextAlternate.Invoke(selectable); // Invoke the context alternate event

        internal void Select() => onSelected.Invoke(); // Invoke the selected event
        internal void Deselect() => onDeselected.Invoke(); // Invoke the selected event

        public void Hover() => onHover.Invoke(); // Invoke the hover event
        public void Unhover() => onUnhover.Invoke(); // Invoke the unhover event
    }


}
