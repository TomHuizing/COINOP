using System;
using Gameplay.Map;
using Gameplay.Units;

namespace Gameplay.Modifiers.UnitRegion
{
    public class PresenceApplicator : IModifierApplicator<UnitController>
    {
        public event Action<IModifier> OnModifier;

        public UnitController Source { get; private set; }

        public PresenceApplicator(UnitController source)
        {
            Source = source != null ? source : throw new ArgumentNullException(nameof(source));
            source.OnCurrentRegionChanged += RegionChanged;
        }

        private void RegionChanged(RegionController controller)
        {
            new PresenceModifier(Source, controller);
        }
    }
}
