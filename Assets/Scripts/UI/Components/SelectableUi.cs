using System;
using System.Collections.Generic;
using Gameplay.Selection;
using UI.Elements;
using UnityEngine;
using ContextMenu = UI.Elements.ContextMenu;

namespace UI.Components
{
    public abstract class SelectableUi<T> : MonoBehaviour, ISelectableUi where T : MonoBehaviour
    {
        [SerializeField] private GameObject selectedUiPrefab;

        private Selectable selectable;

        private readonly Dictionary<Type, IEnumerable<NamedAction>> contextMenus = new();

        private Func<string> getTooltipText;

        protected T Controller { get; private set; }
        protected Selectable Context { get; private set; }
        protected Type ContextType { get; private set; }

        protected virtual void Awake()
        {
            Controller = GetComponent<T>();
            if (Controller == null)
            {
                Debug.LogError($"No component of type {typeof(T).Name} found on {gameObject.name}.");
            }
            selectable = GetComponent<Selectable>();
            if (selectable == null)
            {
                Debug.LogError($"No Selectable component found on {gameObject.name}.");
            }
            selectable.OnContextMenu.AddListener(ShowContextMenu);
            selectable.OnHover.AddListener(ShowTooltip);
            selectable.OnUnhover.AddListener(HideTooltip);
        }

        public void ApplySelectionUi(GameObject parent)
        {
            if (selectedUiPrefab == null)
            {
                Debug.LogWarning("Selected UI prefab is not set.");
                return;
            }

            GameObject selectedUi = Instantiate(selectedUiPrefab, parent.transform);
            if (selectedUi.TryGetComponent(out ISelectionUI<T> selectionUi))
                selectionUi.SelectedObject = Controller;
            selectedUi.transform.SetParent(parent.transform, false);
            selectedUi.transform.localScale = Vector3.one;
            selectedUi.transform.localPosition = Vector3.zero;
            selectedUi.SetActive(true);
        }

        protected void SetTooltipText(string text)
        {
            getTooltipText = () => text;
        }

        protected void SetTooltipText(Func<string> getText)
        {
            getTooltipText = getText;
        }

        protected void SetContextMenu<TContext>(IEnumerable<NamedAction> actions) where TContext : MonoBehaviour
        {
            Type contextType = typeof(TContext);
            contextMenus[contextType] = actions;
        }

        private void ShowContextMenu(Selectable selectable)
        {
            Context = selectable;
            foreach (Type t in contextMenus.Keys)
            {
                if (selectable.TryGetComponent(t, out Component context))
                {
                    ContextType = t;
                    ShowContextMenu(t, context);
                    return;
                }
            }
        }

        private void ShowContextMenu(Type t, Component context)
        {
            if (context == Controller)
                ShowContextMenu();
            else if (contextMenus.TryGetValue(t, out IEnumerable<NamedAction> contextMenu))
                ContextMenu.Instance.Show(contextMenu);
        }

        protected void ShowContextMenu()
        {
            if (contextMenus.TryGetValue(null, out IEnumerable<NamedAction> contextMenu))
                ContextMenu.Instance.Show(contextMenu);
            else
                Debug.LogWarning("No default context menu defined for this selectable UI.");
        }

        protected void ShowTooltip()
        {
            string text = getTooltipText != null ? getTooltipText() : string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                Tooltip.Instance.DescriptionValue = text;
                Tooltip.Instance.Show();
            }
            else
            {
                Tooltip.Instance.Hide();
            }
        }
        
        protected void HideTooltip()
        {
            Tooltip.Instance.Hide();
        }
    }
}
