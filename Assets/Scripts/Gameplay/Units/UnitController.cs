using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private MoveController moveController;

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
    }

    public void ContextClick(Selectable selectable)
    {
        if(selectable.TryGetComponent(out RegionController regionController))
        {
            if(TryGetComponent(out MoveController moveController))
                moveController.SetTarget(regionController);
        }
    }


}
