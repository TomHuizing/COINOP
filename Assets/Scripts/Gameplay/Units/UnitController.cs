using System;
using System.Linq;
using Gameplay.Common;
using Gameplay.Components;
using Gameplay.Map;
using Gameplay.Modifiers;
using Gameplay.Selection;
using UnityEngine;

namespace Gameplay.Units
{
    public class UnitController : MonoBehaviour, IController
    {

        [SerializeField] private MoveController moveController;
        [SerializeField] private ContextMenu contextMenu;

        private UnitModel model;

        public string Name => model.Name;
        public string Description { get; private set; } = "Unit Controller";
        public float Strength => model.Strength;
        public float Supplies => model.Supplies;

        public RegionController TargetRegion => moveController != null ? moveController.Path.LastOrDefault() : null;
        public RegionController NextRegion => moveController != null ? moveController.Path.FirstOrDefault() : null;
        public RegionController CurrentRegion => moveController != null ? moveController.CurrentRegion : null;


        void Start()
        {
            model = new UnitModel(name);
        }

        public void ContextClick(Selectable selectable)
        {
            print($"Context click on {selectable.name}");
            if (selectable.TryGetComponent(out RegionController regionController))
                moveController.SetTarget(regionController);
        }

        public void AddModifier(IModifier modifier)
        {
            throw new NotImplementedException();
        }
    }
}
