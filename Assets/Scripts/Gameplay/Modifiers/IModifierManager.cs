using Gameplay.Common;

namespace Gameplay.Modifiers
{
    public interface IModifierManager
    {
        public bool TryApply(IController source, IController target, out IModifier modifier);
    }
}
