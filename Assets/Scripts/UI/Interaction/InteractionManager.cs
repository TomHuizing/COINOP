using System.Collections.Generic;
using System.Linq;
using Gameplay.Common;
using TMPro;
using UI.Elements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ContextMenu = UI.Elements.ContextMenu;


namespace UI.Interaction
{
    public class InteractionManager : MonoBehaviour
    {
        public static InteractionManager Instance;

        private readonly List<RaycastResult> raycastResults = new();

        private Vector2 mouseStartScreen;
        private Vector2 mouseStartWorld;
        private Vector2 mouseWorld;
        private Vector2 mouseScreen;
        private bool isDragging = false;
        private bool isPrimaryDown = false;
        private Rect dragRect;

        private ILinkable linkableUnderMouseOld;
        private ILinkable linkableUnderMouse;

        private IHoverable hoverableUnderMouseOld;
        private IHoverable hoverableUnderMouse;

        private ITooltippable tooltippableUnderMouseOld;
        private ITooltippable tooltippableUnderMouse;

        [SerializeField] private GraphicRaycaster graphicRaycaster;
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private SelectionBox selectionBox;
        [SerializeField] private float deadZoneScreen;
        [SerializeField] private float deadZoneWorld;

        void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        void Update()
        {
            mouseScreen = Input.mousePosition;
            mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
            hoverableUnderMouseOld = hoverableUnderMouse;
            hoverableUnderMouse = GetAllUnderMouse<IHoverable>().FirstOrDefault();
            tooltippableUnderMouse = GetAllUnderMouse<ITooltippable>().FirstOrDefault();

            if (hoverableUnderMouseOld != hoverableUnderMouse && !isDragging)
            {
                if (hoverableUnderMouseOld != null)
                    hoverableUnderMouseOld.Unhover();
                if (hoverableUnderMouse != null)
                    hoverableUnderMouse.Hover();
            }

            if (tooltippableUnderMouseOld != tooltippableUnderMouse && !isDragging)
            {
                if (tooltippableUnderMouse != null)
                    Tooltip.Instance.Show(tooltippableUnderMouse);
                else
                    Tooltip.Instance.Hide();
                tooltippableUnderMouseOld = tooltippableUnderMouse;
            }

            // linkableUnderMouse = GetLinksUnderMouse().FirstOrDefault();

            // if (linkableUnderMouse != linkableUnderMouseOld && !isDragging)
            // {
            //     if (linkableUnderMouse is ITooltippable tooltippable)
            //         Tooltip.Instance.Show(tooltippable);
            //     else
            //         Tooltip.Instance.Hide();
            //     linkableUnderMouseOld = linkableUnderMouse;
            // }


            CheckMouseButtonsUp();
            CheckMouseButtonsDown();

            if (isPrimaryDown)
                CheckDrag();
        }

        // public void RegisterBoxSelectable(ISelectable selectable) => boxSelectables.Add(selectable);
        // public void DeregisterBoxSelectable(ISelectable selectable) => boxSelectables.Remove(selectable);
        // public bool IsBoxSelectable(ISelectable selectable) => boxSelectables.Contains(selectable);

        private void CheckMouseButtonsUp()
        {
            if (Input.GetMouseButtonUp(0))
                PrimaryMouseUp();
            if (Input.GetMouseButtonUp(1))
                SecondaryMouseUp();
            if (Input.GetMouseButtonUp(2))
                TertiaryMouseUp();

        }

        private void CheckMouseButtonsDown()
        {
            if (IsOverUI())
                return;
            if (Input.GetMouseButtonDown(0))
                    PrimaryMouseDown();
            if (Input.GetMouseButtonDown(1))
                SecondaryMouseDown();
            if (Input.GetMouseButtonDown(2))
                TertiaryMouseDown();
        }

        private void CheckDrag()
        {
            if (isDragging)
                Drag();
            else if (Vector2.Distance(mouseScreen, mouseStartScreen) > deadZoneScreen || Vector2.Distance(mouseWorld, mouseStartWorld) > deadZoneWorld)
                StartDrag();
        }

        private void StartDrag()
        {
            isDragging = true;
            dragRect = Rect.zero;
            if (hoverableUnderMouse != null)
                hoverableUnderMouse.Unhover(); // Unhover the hoverable under mouse to avoid confusion
            Drag();
        }

        private void Drag()
        {
            Vector2 min = Vector2.Min(mouseStartWorld, mouseWorld);
            Vector2 size = Vector2.Max(mouseStartWorld, mouseWorld) - min;
            Rect selectionRect = new(min, size);
            if (selectionRect != dragRect)
            {
                dragRect = selectionRect;
                SelectionManager.instance.Select(GetSelectablesInRect(selectionRect));
                if (selectionBox != null)
                    selectionBox.SetBox(selectionRect);
            }
        }

        private void EndDrag()
        {
            isDragging = false;
            if (selectionBox != null)
                selectionBox.Hide();
        }

        private void PrimaryMouseDown()
        {
            isPrimaryDown = true;
            mouseStartScreen = mouseScreen;
            mouseStartWorld = mouseWorld;
        }

        private void SecondaryMouseDown()
        {
            

            // selectableUnderMouse.ContextMenu(selectableUnderMouse);
            // selectableUnderMouse.ContextAlternate(selectableUnderMouse);
        }

        private void TertiaryMouseDown()
        {

        }

        private void PrimaryMouseUp()
        {
            if (!isPrimaryDown)
                return;
            isPrimaryDown = false;
            if (!isDragging)
                PrimaryClick();
            else
                EndDrag();
        }

        private void SecondaryMouseUp()
        {
            if (!isDragging)
                SecondaryClick();
        }

        private void TertiaryMouseUp()
        {

        }

        private void PrimaryClick()
        {
            ClickSelect();
            var clickable = GetAllUnderMouse<IClickable>().FirstOrDefault();
            if (clickable != null)
                clickable.Click(GetKeyModifiers());
        }

        private void SecondaryClick()
        {
            IEnumerable<IContextable> contextable = SelectionManager.instance.Selection
                .Select(s => s as MonoBehaviour)
                .Where(m => m != null)
                .Select(m => m.GetComponent<IContextable>())
                .Where(c => c != null);
            if (contextable.Count() == 0)
            {
                contextable = GetAllUnderMouse<IContextable>();
                HandleContext(contextable, null);
                return;
            }
            HandleContext(contextable, GetAllUnderMouse<IInteractable>()?.FirstOrDefault()?.Controller);
        }

        private void ClickSelect()
        {
            var modifiers = GetKeyModifiers();
            if (modifiers == KeyModifiers.None)
            {
                var selectables = GetAllUnderMouse<ISelectable>().Where(s => s.IsClickSelectable);
                print(selectables.Count());
                if (selectables.Count() == 1)
                    selectables.First().Select();
                else if (selectables.Count() > 1)
                    SelectionManager.instance.Select(selectables.Where(s => s.IsMultiSelectable));
                else
                    SelectionManager.instance.DeselectAll();
            }
            else if (modifiers.HasFlag(KeyModifiers.Shift))
            {
                var selectables = GetAllUnderMouse<ISelectable>().Where(s => s.IsClickSelectable && s.IsMultiSelectable);
                SelectionManager.instance.AddToSelection(selectables);
            }
            else if (modifiers.HasFlag(KeyModifiers.Ctrl))
            {
                var selectables = GetAllUnderMouse<ISelectable>().Where(s => s.IsClickSelectable && s.IsSelected);
                SelectionManager.instance.Deselect(selectables);
            }
        }

        private void HandleContext(IEnumerable<IContextable> contextables, IController context)
        {
            var modifiers = GetKeyModifiers();
            if (modifiers.HasFlag(KeyModifiers.Ctrl))
            {
                if (contextables.Count() == 1)
                    ContextMenu.Instance.Show(contextables.First().GetContextMenu(context));
            }
            else if (modifiers.HasFlag(KeyModifiers.Shift))
            {
                foreach (var contextable in contextables)
                {
                    contextable.ContextAlt(context);
                }
            }
            else
            {
                foreach (var contextable in contextables)
                {
                    contextable.ContextClick(context);
                }
            }
        }

        private bool IsOverUI()
        {
            if (!EventSystem.current.IsPointerOverGameObject() || graphicRaycaster == null || eventSystem == null)
                return false;
            if (GetLinksUnderMouse<ILinkable>().Count() == 0)
                return true;
            return false;
            
        }

        private IEnumerable<T> GetAllUnderMouse<T>() where T : IInteractable
            => GetObjectsUnderMouse<T>().Union(GetLinksUnderMouse<T>()).Distinct();

        private IEnumerable<T> GetObjectsUnderMouse<T>() where T : IInteractable
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return Enumerable.Empty<T>();
            var objects = Physics2D.RaycastAll(mouseWorld, Vector2.zero, Mathf.Infinity)
                .Where(h => h.collider != null)
                .Select(h => h.collider.GetComponent<T>())
                .Where(s => s != null)
                .ToList();
            if (objects.Count <= 1)
                return objects;
            InteractionLayer layer = objects.Max(s => s.Layer);
            return objects.Where(s => s.Layer == layer);
        }

        private IEnumerable<T> GetLinksUnderMouse<T>() where T : IInteractable
        {
            if (!EventSystem.current.IsPointerOverGameObject() || graphicRaycaster == null || eventSystem == null)
                return Enumerable.Empty<T>();
            raycastResults.Clear();
            graphicRaycaster.Raycast(new PointerEventData(eventSystem) { position = Input.mousePosition }, raycastResults);
            IEnumerable<TextMeshProUGUI> tmps = raycastResults.Select(r => r.gameObject.GetComponent<TextMeshProUGUI>()).Where(t => t != null);
            return GetIdsUnderMouse(tmps).Select(s => ILinkable.GetLinkable(s)).Where(l => l is T).Select(l => (T)l);
        }

        private IEnumerable<string> GetIdsUnderMouse(IEnumerable<TextMeshProUGUI> tmps) =>
            tmps.Select(t =>
            {
                int linkIndex = TMP_TextUtilities.FindIntersectingLink(t, mouseScreen, null);
                if (linkIndex < 0)
                    return string.Empty;
                return t.textInfo.linkInfo[linkIndex].GetLinkID();
            }).Where(s => !string.IsNullOrEmpty(s));

        private IEnumerable<ISelectable> GetSelectablesInRect(Rect rect)
            => ISelectable.BoxSelectables.Where(s => s.IsMultiSelectable && rect.Contains(s.Position));

        private KeyModifiers GetKeyModifiers()
        {
            var ret = KeyModifiers.None;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                ret |= KeyModifiers.Shift;
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                ret |= KeyModifiers.Ctrl;
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                ret |= KeyModifiers.Alt;
            return ret;
        }
    }
}
