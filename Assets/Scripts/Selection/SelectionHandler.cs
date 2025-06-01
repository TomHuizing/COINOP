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
    private bool isDragging = false;
    private bool mouseDown = false;

    [SerializeField] SelectionManager selectionManager;
    [SerializeField] private float deadZoneScreen;
    [SerializeField] private float deadZoneWorld;

    void Update()
    {
        if(mouseDown)
    }

    public void Select(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;
        print("Click");
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        selectionManager.Select(GetSelectableFromHits(Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero, Mathf.Infinity)));
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
