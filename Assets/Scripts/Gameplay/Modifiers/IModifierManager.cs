using Gameplay.Common;

namespace Gameplay.Modifiers
{
    public interface IModifierManager
    {
        public bool ConditionsMet(IController source, IController target);
        public IModifier Apply(IController source, IController target);
    }
}
