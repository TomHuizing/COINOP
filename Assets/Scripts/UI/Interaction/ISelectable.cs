using System;
using System.Linq;
using UnityEngine;

namespace UI.Interaction
{
    public interface ISelectable : IInteractable
    {
        public Vector2 Position { get; }

        public bool IsClickSelectable { get; }
        public bool IsMultiSelectable { get; }

        public bool IsSelected => SelectionManager.instance.Selection.Contains(this);

        public event Action<ISelectable> OnSelected;
        public event Action<ISelectable> OnDeselected;

        public void Select() { SelectionManager.instance.Select(this); }
        public void Deselect() { SelectionManager.instance.Deselect(this); }

        public void SelectCallback();
        public void DeselectCallback();

        public void ApplySelectionUi(Transform parent);
    }
}
