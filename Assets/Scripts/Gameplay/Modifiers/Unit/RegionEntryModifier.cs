using System;
using Gameplay.Map;
using Gameplay.Time;
using Gameplay.Units;

namespace Gameplay.Modifiers.Unit
{
    public class RegionEntryModifier : IRegionModifier<UnitController>
    {
        private readonly RegionStats transientStats = new RegionStats().SetInsurgencyPresence(-0.5f).SetPopularSupport(0.2f);
        private readonly Lerp lerp = new(TimeSpan.FromHours(6));

        public RegionStats Stats => transientStats.Decay(lerp.Value);

        public string SourceName => Source.Name;
        public string TargetName => Target.Name;

        public string Description => $"{SourceName} has patrolled {TargetName}";

        public string ModifierValues => Stats.ToString();

        public ModifierPersistence Persistence { get; private set; } = ModifierPersistence.Transient;

        public UnitController Source { get; private set; }
        public RegionController Target { get; private set; }

        public DateTime TimeOfExpiry => lerp.End;

        public bool Expired => lerp.Value >= 1;

        public RegionEntryModifier(UnitController source, RegionController target)
        {
            Source = source != null ? source : throw new ArgumentNullException(nameof(source));
            Target = target != null ? target : throw new ArgumentNullException(nameof(target));
            Target.AddModifier(this);
        }
    }
}
