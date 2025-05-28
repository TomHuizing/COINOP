using System;
using System.Collections.Generic;
using System.Linq;
using InputSystem;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private Dictionary<string, Action<RegionController>> regionContextActions;

    [SerializeField] private MoveController moveController;
    [SerializeField] private ContextMenu contextMenu;

    private UnitModel model;

    public string Name => model.Name;
    public float Strength => model.Strength;
    public float Supplies => model.Supplies;

    public RegionController TargetRegion => moveController != null ? moveController.Path.LastOrDefault() : null;
    public RegionController NextRegion => moveController != null ? moveController.Path.FirstOrDefault() : null;
    public RegionController CurrentRegion => moveController != null ? moveController.CurrentRegion : null;

    public string Description => throw new System.NotImplementedException();

    void Start()
    {
        model = new UnitModel(name);
        regionContextActions = new()
        {
            { "Patrol Here", r => moveController.SetTarget(r) },
            { "Plan Mission", r => Debug.Log($"Planning mission in region {r.name}") }
        };
    }

    public void ContextClick(Selectable selectable)
    {
        if (selectable.TryGetComponent(out RegionController regionController))
            moveController.SetTarget(regionController);
    }

    public void ShowMenu(Selectable selectable)
    {
        if (!selectable.TryGetComponent(out RegionController regionController))
            return;
        ContextMenuManager.Instance.ShowContextMenu(new()
        {
            {"Move Here", () => moveController.SetTarget(regionController)}
        });
    }


}
