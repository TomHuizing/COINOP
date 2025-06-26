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
        public string ModifierValues => Stats.ToString();
        public RegionStats Stats => startingStats.Decay(lerp.Value);
        public UnitController Source { get; private set; }
        public RegionController Target { get; private set; }

        private readonly Lerp lerp;
        private readonly RegionStats startingStats = new(0.1f, 0f, -0.1f, 0.1f, 0.1f, 0.2f, 0f);

        public PresenceModifier(UnitController source, RegionController target)
        {
            lerp = new Lerp(TimeSpan.FromHours(6));
            Source = source;
            Target = target;
            Description = $"Presence of {SourceName} in {TargetName} increases support and infrastructure, while reducing control.";
            lerp.OnEnd += Destroy;
        }

        public void Destroy()
        {
            Target.RemoveModifier(this);
        }
    }
}
