using System;
using Gameplay.Map;
using Gameplay.Time;
using Gameplay.Units;

namespace Gameplay.Modifiers.UnitRegion
{
    public class PresenceModifier : IRegionModifier<UnitController>
    {
        public string SourceName => Source.Name;
        public string TargetName => Target.Name;
        public string Description { get; private set; }
        public string ModifierValues => StatModifiers.ToString();
        public RegionStats StatModifiers { get; private set; } = new(0.1f, 0f, -0.1f, 0.1f, 0.1f, 0.2f, 0f);
        public UnitController Source { get; private set; }
        public RegionController Target { get; private set; }


        public event Action OnRemove;

        public PresenceModifier(UnitController source, RegionController target, Decay decay)
        {
            Source = source;
            Target = target;
            Description = $"Presence of {SourceName} in {TargetName} increases support and infrastructure, while reducing control.";
            decay.OnEnd += () => OnRemove?.Invoke();
        }
    }
}
