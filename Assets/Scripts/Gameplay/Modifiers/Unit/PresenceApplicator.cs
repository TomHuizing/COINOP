using System;
using Gameplay.Units;

namespace Gameplay.Modifiers.UnitRegion
{
    public class PresenceApplicator : IModifierApplicator<UnitController>
    {
        private PresenceModifier linkedModifier;

        public UnitController Source { get; private set; }

        public PresenceApplicator(UnitController source)
        {
            Source = source != null ? source : throw new ArgumentNullException(nameof(source));
        }

        public bool TryApply(out IModifier modifier)
        {
            if (linkedModifier == null || Source.CurrentRegion != linkedModifier.Target)
            {
                linkedModifier = new(Source, Source.CurrentRegion);
                
                modifier = linkedModifier;
                return true;
            }
            modifier = null;
            return false;
        }
    }
}
