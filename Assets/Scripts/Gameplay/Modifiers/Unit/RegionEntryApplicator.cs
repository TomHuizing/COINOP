using System;
using Gameplay.Map;
using Gameplay.Units;

namespace Gameplay.Modifiers.Unit
{
    public class RegionEntryApplicator : IModifierApplicator<UnitController>
    {
        private RegionController target;

        public UnitController Source { get; private set; }

        public RegionEntryApplicator(UnitController source)
        {
            Source = source != null ? source : throw new ArgumentNullException(nameof(source));
        }

        public bool TryApply(out IModifier modifier)
        {
            if (target != Source.CurrentRegion)
            {
                target = Source.CurrentRegion;
                modifier = new RegionEntryModifier(Source, target);
                return true;
            }
            modifier = null;
            return false;
        }
    }
}
