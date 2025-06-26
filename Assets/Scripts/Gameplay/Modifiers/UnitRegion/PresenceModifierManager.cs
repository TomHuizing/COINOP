using System;
using Gameplay.Common;
using Gameplay.Map;
using Gameplay.Time;
using Gameplay.Units;

namespace Gameplay.Modifiers.UnitRegion
{
    public class PresenceModifierManager : IModifierManager
    {
        private PresenceModifier linkedModifier;

        public IModifier Apply(IController source, IController target)
        {
            throw new NotImplementedException();
        }

        public bool TryApply(IController source, IController target, out IModifier modifier)
        {
            if (ConditionsMet(source, target))
            {
                linkedModifier?.Destroy(); // Remove existing modifier if it exists
                linkedModifier = new PresenceModifier(source as UnitController, target as RegionController);
                (target as RegionController).AddModifier(linkedModifier);
                modifier = linkedModifier;
                return true;
            }
            else
            {
                modifier = null;
                return false;
            }
        }

        private bool ConditionsMet(IController source, IController target)
        {
            return source is UnitController unitController &&
                target is RegionController regionController &&
                unitController.CurrentRegion == regionController &&
                linkedModifier?.Target != regionController;
        }
    }
}
