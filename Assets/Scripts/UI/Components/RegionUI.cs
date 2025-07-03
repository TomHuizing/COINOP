using System;
using System.Collections.Generic;
using Gameplay.Common;
using Gameplay.Map;
using UI.Interaction;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Components
{
    // public class RegionUI : SelectableUi<RegionController>
    [RequireComponent(typeof(RegionController))]
    public class RegionUI : MonoBehaviour,
        ISelectable,
        IContextable,
        ITooltippable,
        IHoverable
    {
        private RegionController controller;

        [SerializeField] private RegionSelectionUI selectionUIPrefab;
        [SerializeField] private UnityEvent onSelected;
        [SerializeField] private UnityEvent onDeselected;
        [SerializeField] private UnityEvent onHover;
        [SerializeField] private UnityEvent onUnhover;

        public Vector2 Position => transform.position;
        public IController Controller => controller;
        public InteractionLayer Layer { get; } = InteractionLayer.Map;

        public bool IsClickSelectable { get; } = true;
        public bool IsMultiSelectable { get; } = false;
        public bool IsBoxSelectable { get; } = false;

        public string TooltipText => controller.Name;
        public bool IsHovered { get; private set; } = false;

        public event Action<ISelectable> OnSelected;
        public event Action<ISelectable> OnDeselected;
        public event Action<IHoverable> OnHover;
        public event Action<IHoverable> OnUnhover;

        void Start()
        {
            controller = GetComponent<RegionController>();

            OnSelected += _ => onSelected.Invoke();
            OnDeselected += _ => onDeselected.Invoke();
            OnHover += _ => onHover.Invoke();
            OnUnhover += _ => onUnhover.Invoke();
        }

        public void ApplySelectionUi(Transform parent)
        {
            var selectionUi = Instantiate(selectionUIPrefab, parent);
            selectionUi.SelectedObject = controller;
        }

        public IEnumerable<IContextMenuItem> GetContextMenu(IController context)
        {
            return new IContextMenuItem[] { new CreateMissionItem() };
        }

        public void DeselectCallback() => OnDeselected?.Invoke(this);
        public void SelectCallback() => OnSelected?.Invoke(this);

        public void Hover()
        {
            IsHovered = true;
            OnHover?.Invoke(this);
        }

        public void Unhover()
        {
            IsHovered = false;
            OnUnhover?.Invoke(this);
        }

        public class CreateMissionItem : IContextMenuItem
        {
            public string Text => "Create mission";
            public bool Enabled => true;
            public bool Shown => true;

            public void Select() => print("Creating mission");
        }
    }
}
