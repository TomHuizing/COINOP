using System;
using Gameplay.Common;
using Gameplay.Map;
using Gameplay.Units;

namespace Gameplay.Modifiers.UnitRegion
{
    public class PresenceModifierManager : IModifierManager
    {
        private PresenceModifier modifier;

        public IModifier Apply(IController source, IController target)
        {
            throw new NotImplementedException();
        }

        public bool ConditionsMet(IController source, IController target)
            => modifier == null &&
                source is UnitController unitController &&
                target is RegionController regionController &&
                unitController.CurrentRegion == regionController;
    }
}
