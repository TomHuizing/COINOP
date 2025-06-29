using System;
using System.Collections.Generic;
using Gameplay.Modifiers.Unit;
using Gameplay.Time;
using Gameplay.Units;

namespace Gameplay.Modifiers.UnitRegion
{
    public class UnitModifierManager : IModifierManager<UnitController>
    {
        private readonly List<IModifierApplicator<UnitController>> applicators = new();

        public UnitController Source { get; private set; }
        public IEnumerable<IModifierApplicator<UnitController>> Applicators => applicators;

        public UnitModifierManager(UnitController controller)
        {
            Source = controller != null ? controller : throw new ArgumentNullException(nameof(controller));
            GameClock.instance.OnTick += Tick;
            applicators.Add(new PresenceApplicator(Source));
            applicators.Add(new RegionEntryApplicator(Source));
        }

        public void AddApplicator(IModifierApplicator<UnitController> applicator) => applicators.Add(applicator);
        public bool RemoveApplicator(IModifierApplicator<UnitController> applicator) => applicators.Remove(applicator);

        public void Tick(DateTime now, TimeSpan delta)
        {
            // foreach (var applicator in applicators)
            // {
            //     if (applicator.TryApply(out IModifier modifier))
            //     {
            //         UnityEngine.Debug.Log($"{modifier.SourceName} -> {modifier.TargetName}: {modifier.Description}");
            //     }
            // }
        }
    }

}
