using System.Collections.Generic;
using System.Linq;
using Gameplay.Selection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Selection
{
    public class SelectionHandler : MonoBehaviour
    {
        private Vector2 mouseStartScreen;
        private Vector2 mouseStartWorld;
        private Vector2 mouseWorld;
        private Vector2 mouseScreen;
        private bool isDragging = false;
        private bool isPrimaryDown = false;
        private Rect dragRect;

        private Selectable selectableUnderMouseOld;
        private Selectable selectableUnderMouse;

        [SerializeField] private SelectionBox selectionBox;
        [SerializeField] private SelectionManager selectionManager;
        [SerializeField] private float deadZoneScreen;
        [SerializeField] private float deadZoneWorld;



        void Update()
        {
            mouseScreen = Input.mousePosition;
            mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
            selectableUnderMouseOld = selectableUnderMouse;
            selectableUnderMouse = GetSelectablesUnderMouse().FirstOrDefault();

            if (selectableUnderMouseOld != selectableUnderMouse && !isDragging)
            {
                if (selectableUnderMouseOld != null)
                    selectableUnderMouseOld.Unhover();
                if (selectableUnderMouse != null)
                    selectableUnderMouse.Hover();
            }
        
            CheckMouseButtonsUp();
            CheckMouseButtonsDown();

            if (isPrimaryDown)
                CheckDrag();
        }

        public void CheckMouseButtonsUp()
        {
            if (Input.GetMouseButtonUp(0))
                PrimaryMouseUp();
            if (Input.GetMouseButtonUp(1))
                SecondaryMouseUp();
            if (Input.GetMouseButtonUp(2))
                TertiaryMouseUp();
        
        }

        public void CheckMouseButtonsDown()
        {
            if(EventSystem.current.IsPointerOverGameObject())
                return;
            if (Input.GetMouseButtonDown(0))
                PrimaryMouseDown();
            if (Input.GetMouseButtonDown(1))
                SecondaryMouseDown();
            if (Input.GetMouseButtonDown(2))
                TertiaryMouseDown();
        }

        public void CheckDrag()
        {
            if(isDragging)
                Drag();
            else if (Vector2.Distance(mouseScreen, mouseStartScreen) > deadZoneScreen || Vector2.Distance(mouseWorld, mouseStartWorld) > deadZoneWorld)
                StartDrag();
        }

        private void StartDrag()
        {
            isDragging = true;
            dragRect = Rect.zero;
            if(selectableUnderMouse != null)
                selectableUnderMouse.Unhover(); // Unhover the selectable under mouse to avoid confusion
            Drag();
        }

        private void Drag()
        {
            Vector2 min = Vector2.Min(mouseStartWorld, mouseWorld);
            Vector2 size = Vector2.Max(mouseStartWorld, mouseWorld) - min;
            Rect selectionRect = new(min, size);
            if(selectionRect != dragRect)
            {
                dragRect = selectionRect;
                selectionManager.Select(GetSelectablesInRect(selectionRect));
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
            if (selectableUnderMouse == null)
                return;
            selectionManager.Selected.FirstOrDefault()?.ContextClick(selectableUnderMouse); //F/out
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
                ClickSelect();
            else
                EndDrag();
        }

        private void SecondaryMouseUp()
        {

        }

        private void TertiaryMouseUp()
        {
        
        }

        private void ClickSelect()
        {
            var selectable = GetSelectablesUnderMouse().FirstOrDefault();
            if (selectable == null)
                selectionManager.DeselectAll();
            else
                selectionManager.Select(selectable);
        }

        private IEnumerable<Selectable> GetSelectablesUnderMouse()
        {
            var selectables = Physics2D.RaycastAll(mouseWorld, Vector2.zero, Mathf.Infinity)
                .Where(h => h.collider != null)
                .Select(h => h.collider.GetComponent<Selectable>())
                .Where(s => s != null)
                .ToList();
            if (selectables.Count <= 1)
                return selectables;
            var layer = selectables.Max(s => s.Layer);
            return selectables.Where(s => s.Layer == layer);
        }

        private IEnumerable<Selectable> GetSelectablesInRect(Rect rect)
            => Selectable.Selectables.Where(s => s.IsMultiSelectable && rect.Contains(s.transform.position));
    }
}
