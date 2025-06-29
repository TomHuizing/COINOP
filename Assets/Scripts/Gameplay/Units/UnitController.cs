using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Common;
using Gameplay.Components;
using Gameplay.Map;
using Gameplay.Modifiers;
using Gameplay.Modifiers.Unit;
using Gameplay.Modifiers.UnitRegion;
using Gameplay.Selection;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Gameplay.Units
{
    public class UnitController : MonoBehaviour, IController
    {
        [SerializeField] private MoveController moveController;
        [SerializeField] private ContextMenu contextMenu;

        private UnitModel model;
        // private UnitModifierManager modifierManager;
        private readonly List<IModifierApplicator<UnitController>> modifierApplicators = new();

        public event Action OnCreated;
        public event Action OnDestroyed;

        public event Action<RegionController> OnCurrentRegionChanged;
        public event Action<IEnumerable<RegionController>> OnPathChanged;

        

        public string Name => model.Name;
        public string Description { get; } = "Unit Controller";
        public string Id => Name;

        public float Strength => model.Strength;
        public float Supplies => model.Supplies;

        public RegionController TargetRegion => moveController != null ? moveController.Path.LastOrDefault() : null;
        public RegionController NextRegion => moveController != null ? moveController.Path.FirstOrDefault() : null;
        public RegionController CurrentRegion => moveController != null ? moveController.CurrentRegion : null;

        void Start()
        {

            moveController.OnCurrentRegionChanged += (r) => OnCurrentRegionChanged?.Invoke(r);
            moveController.OnPathChanged += (p) => OnPathChanged?.Invoke(p);
            model = new UnitModel(name);
            // modifierManager = new(this);

            modifierApplicators.Add(new RegionEntryApplicator(this));
            modifierApplicators.Add(new PresenceApplicator(this));
            modifierApplicators.Add(new UnitResourceApplicator(this));

            IController.Lookup[Id] = this;
            OnCreated?.Invoke();
        }

        void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }

        public void ContextClick(Selectable selectable)
        {
            if (selectable.TryGetComponent(out RegionController regionController))
                moveController.SetTarget(regionController);
        }

        public void AddModifier(IModifier modifier)
        {
            throw new NotImplementedException();
        }

        public bool RemoveModifier(IModifier modifier)
        {
            throw new NotImplementedException();
        }
    }
}
