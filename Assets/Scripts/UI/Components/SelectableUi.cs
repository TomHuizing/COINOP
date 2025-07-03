// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Gameplay.Selection;
// using UI.Elements;
// using UnityEngine;
// using UnityEngine.Events;

// namespace UI.Components
// {
//     [RequireComponent(typeof(Selectable))]
//     public abstract class SelectableUi<T> : MonoBehaviour, ISelectableUi where T : MonoBehaviour
//     {
//         [SerializeField] private GameObject selectedUiPrefab;
//         [SerializeField] private UnityEvent<Selectable> onContextClick = new();
//         [SerializeField] private UnityEvent<Selectable> onContextAlternate = new();
//         [SerializeField] private UnityEvent onHover = new(); // Event triggered when the object is hovered over
//         [SerializeField] private UnityEvent onUnhover = new(); // Event triggered when the object is unhovered

//         private Selectable selectable;

//         private readonly Dictionary<Type, IEnumerable<NamedAction>> contextMenus = new();

//         private Func<string> getTooltipText;

//         protected T Controller { get; private set; }
//         protected Selectable Context { get; private set; }
//         protected Type ContextType { get; private set; }

//         protected virtual void Awake()
//         {
//             if (TryGetComponent<T>(out var controller))
//                 Controller = controller;
//             else
//                 Debug.LogError($"No component of type {typeof(T).Name} found on {gameObject.name}.");
//             selectable = GetComponent<Selectable>();
//             selectable.OnContextMenu.AddListener(ShowContextMenu);
//             selectable.OnHover.AddListener(ShowTooltip);
//             selectable.OnUnhover.AddListener(HideTooltip);
//         }

//         public void ContextClick(Selectable selectable) => onContextClick.Invoke(selectable); // Invoke the context click event
//         public void ContextAlternate(Selectable selectable) => onContextAlternate.Invoke(selectable); // Invoke the context alternate event
//         public void ContextMenu(Selectable selectable) => ShowContextMenu(selectable);

//         public void Hover()
//         {
//             onHover.Invoke(); // Invoke the hover event
//             ShowTooltip();
//         }

//         public void Unhover()
//         {
//             onUnhover.Invoke(); // Invoke the unhover event
//             HideTooltip();
//         }

//         public void ApplySelectionUi(GameObject parent)
//         {
//             if (selectedUiPrefab == null)
//             {
//                 Debug.LogWarning("Selected UI prefab is not set.");
//                 return;
//             }

//             GameObject selectedUi = Instantiate(selectedUiPrefab, parent.transform);
//             if (selectedUi.TryGetComponent(out ISelectionUI<T> selectionUi))
//                 selectionUi.SelectedObject = Controller;
//             selectedUi.transform.SetParent(parent.transform, false);
//             selectedUi.transform.localScale = Vector3.one;
//             selectedUi.transform.localPosition = Vector3.zero;
//             selectedUi.SetActive(true);
//         }

//         protected void SetTooltipText(string text)
//         {
//             getTooltipText = () => text;
//         }

//         protected void SetTooltipText(Func<string> getText)
//         {
//             getTooltipText = getText;
//         }

//         protected void SetContextMenu<TContext>(IEnumerable<NamedAction> actions) where TContext : MonoBehaviour
//         {
//             Type contextType = typeof(TContext);
//             contextMenus[contextType] = actions;
//         }

//         private void ShowContextMenu(Selectable selectable)
//         {
//             Context = selectable;
//             Component contextComponent = null;
//             ContextType = contextMenus.Keys.FirstOrDefault(t => selectable.TryGetComponent(t, out contextComponent));
//             if (ContextType != null)
//                 ShowContextMenu(ContextType, contextComponent);
//         }

//         private void ShowContextMenu(Type t, Component context)
//         {
//             if (context == Controller)
//                 ShowContextMenu();
//             else if (contextMenus.TryGetValue(t, out IEnumerable<NamedAction> contextMenu))
//                 Elements.ContextMenu.Instance.Show(contextMenu);
//         }

//         protected void ShowContextMenu()
//         {
//             if (contextMenus.TryGetValue(null, out IEnumerable<NamedAction> contextMenu))
//                 Elements.ContextMenu.Instance.Show(contextMenu);
//             else
//                 Debug.LogWarning("No default context menu defined for this selectable UI.");
//         }

//         protected void ShowTooltip()
//         {
//             string text = getTooltipText != null ? getTooltipText() : string.Empty;
//             if (!string.IsNullOrEmpty(text))
//             {
//                 Tooltip.Instance.DescriptionValue = text;
//                 Tooltip.Instance.Show();
//             }
//             else
//             {
//                 Tooltip.Instance.Hide();
//             }
//         }

//         protected void HideTooltip()
//         {
//             Tooltip.Instance.Hide();
//         }
//     }
// }
