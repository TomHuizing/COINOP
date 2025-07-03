using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Common;
using Gameplay.Components;
using Gameplay.Map;
using Gameplay.Units;
using UI.Interaction;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Components
{
    [RequireComponent(typeof(MoveController))]
    [RequireComponent(typeof(UnitController))]
    //public class UnitUI : SelectableUi<UnitController>
    public class UnitUI : MonoBehaviour,
        ISelectable,
        IContextable,
        ITooltippable,
        IHoverable

    {
        [SerializeField] private UnitSelectionUi selectionUiPrefab;
        [SerializeField] private UnityEvent onSelected;
        [SerializeField] private UnityEvent onDeselected;
        [SerializeField] private UnityEvent onHover;
        [SerializeField] private UnityEvent onUnhover;

        private MoveController moveController;
        private UnitController controller;

        public Vector2 Position => transform.position;
        public IController Controller => controller;

        public InteractionLayer Layer { get; } = InteractionLayer.Object;

        public string TooltipText => controller.Name;

        public bool IsSelected => SelectionManager.instance.Selection.Contains(this);
        public bool IsMultiSelectable { get; } = true;
        public bool IsClickSelectable { get; } = true;

        public bool IsHovered { get; private set; }

        public event Action<ISelectable> OnSelected;
        public event Action<ISelectable> OnDeselected;
        public event Action<IHoverable> OnHover;
        public event Action<IHoverable> OnUnhover;

        void Start()
        {
            controller = GetComponent<UnitController>();
            moveController = GetComponent<MoveController>();

            OnSelected += _ => onSelected.Invoke();
            OnDeselected += _ => onDeselected.Invoke();
            OnHover += _ => onHover.Invoke();
            OnUnhover += _ => onUnhover.Invoke();
            InteractionManager.Instance.RegisterBoxSelectable(this);
        }

        public void ApplySelectionUi(Transform parent)
        {
            var selectionUi = Instantiate(selectionUiPrefab, parent);
            selectionUi.SelectedObject = controller;
        }

        public void SelectCallback() => OnSelected?.Invoke(this);
        public void DeselectCallback() => OnDeselected?.Invoke(this);

        public void ContextAlt(IController context)
        {
            if (context is RegionController regionController)
            {
                moveController.AddTarget(regionController);
            }
        }

        public void ContextClick(IController context)
        {
            if (context is RegionController regionController)
            {
                moveController.SetTarget(regionController);
            }
        }

        public IEnumerable<IContextMenuItem> GetContextMenu(IController context)
        {
            if (context is RegionController regionController)
                return new IContextMenuItem[] { new MoveItem(moveController, regionController) };
            else
                return Enumerable.Empty<IContextMenuItem>();
        }

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

        private class MoveItem : IContextMenuItem
        {
            private RegionController regionController;
            private MoveController moveController;

            public string Text => $"Move to {regionController.Name}";
            public bool Enabled => regionController.enabled;
            public bool Shown { get; } = true;

            public MoveItem(MoveController moveController, RegionController regionController)
            {
                this.moveController = moveController;
                this.regionController = regionController;
            }

            public void Select() => moveController.SetTarget(regionController);
        }
    }
}
