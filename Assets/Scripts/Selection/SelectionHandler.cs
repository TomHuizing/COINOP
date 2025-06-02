using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SelectionHandler : MonoBehaviour
{
    private Vector2 mouseStartScreen;
    private Vector2 mouseStartWorld;
    private Vector2 mouseWorld;
    private Vector2 mouseScreen;
    private bool isDragging = false;
    private bool isPrimaryDown = false;

    [SerializeField] SelectionManager selectionManager;
    [SerializeField] private float deadZoneScreen;
    [SerializeField] private float deadZoneWorld;

    public void Select(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;
        print("Click");
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
    }

    public void MouseMove(InputAction.CallbackContext context)
    {
        mouseScreen = context.ReadValue<Vector2>();
        mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        if (isPrimaryDown && !isDragging)
        {
            if (Vector2.Distance(mouseScreen, mouseStartScreen) > deadZoneScreen || Vector2.Distance(mouseWorld, mouseStartWorld) > deadZoneWorld)
                isDragging = true;
        }
    }

    public void PrimaryMouseButton(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                PrimaryMouseDown();
                break;
            case InputActionPhase.Canceled:
                PrimaryMouseUp();
                break;
            default:
                break;
        }
    }

    private void PrimaryMouseDown()
    {
        isPrimaryDown = true;
        mouseStartScreen = mouseScreen;
        mouseStartWorld = mouseWorld;
    }

    private void PrimaryMouseUp()
    {
        isPrimaryDown = false;
        if (isDragging)
            EndDrag();
        else
            ClickSelect();
    }

    private void ClickSelect()
    {
        selectionManager.Select(GetSelectableFromHits(Physics2D.RaycastAll(mouseWorld, Vector2.zero, Mathf.Infinity)));
    }

    private void StartDrag()
    {

    }

    private void EndDrag()
    {
        isDragging = false;
        Vector2 min = Vector2.Min(mouseStartWorld, mouseWorld);
        Vector2 size = Vector2.Max(mouseStartWorld, mouseWorld) - min;
        Rect selectionRect = new(min, size);
        selectionManager.Select(GetSelectablesInRect(selectionRect));
    }

    private Selectable GetSelectableFromHits(RaycastHit2D[] hits)
    {
        IEnumerable<Selectable> selectables = hits
            .Where(h => h.collider != null)
            .Select(h => h.collider.GetComponent<Selectable>())
            .Where(s => s != null);
        if (selectables.Count() == 0)
            return null;
        if (selectables.Count() == 1)
            return selectables.First();
        var layer = selectables.Max(s => s.Layer);
        var selectablesInLayer = selectables.Where(s => s.Layer == layer);
        return selectablesInLayer.First();
    }

    private IEnumerable<Selectable> GetSelectablesInRect(Rect rect)
        => Selectable.Selectables.Where(s => s.IsMultiSelectable && rect.Contains(s.transform.position));
}
