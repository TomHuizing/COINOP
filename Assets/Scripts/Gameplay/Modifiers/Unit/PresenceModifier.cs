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

        public ModifierPersistence Persistence => ModifierPersistence.Sustained;

        //public RegionStats Stats => startingStats.Decay(lerp.Value);
        public RegionStats Stats { get; } = new(0.1f, 0f, -0.1f, 0.1f, 0.1f, 0.2f, 0f);
        public UnitController Source { get; private set; }
        public RegionController Target { get; private set; }

        public DateTime TimeOfExpiry => DateTime.MinValue;
        public bool Expired { get; private set; } = false;


        // private readonly Lerp lerp;
        // private readonly RegionStats startingStats = new(0.1f, 0f, -0.1f, 0.1f, 0.1f, 0.2f, 0f);

        public PresenceModifier(UnitController source, RegionController target)
        {
            Source = source;
            Target = target;
            Description = $"Presence of {SourceName} in {TargetName} increases support and infrastructure, while reducing control.";
            Target.AddModifier(this);
            GameClock.instance.OnTick += Validate;
        }

        private void Validate(DateTime now, TimeSpan delta)
        {
            if (Source.CurrentRegion != Target)
              Destroy();
        }

        public void Destroy()
        {
            Expired = true;
        }
    }
}
