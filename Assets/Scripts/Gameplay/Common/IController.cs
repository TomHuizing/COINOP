using Gameplay.Modifiers;

namespace Gameplay.Common
{
    public interface IController
    {
        public string Name { get; }
        public string Description { get; }

        public void AddModifier(IModifier modifier);
    }
}
