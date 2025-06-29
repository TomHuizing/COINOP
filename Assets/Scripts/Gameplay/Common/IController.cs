using System.Collections.Generic;
using Gameplay.Modifiers;

namespace Gameplay.Common
{
    public interface IController
    {
        public static Dictionary<string, IController> Lookup = new();

        public string Name { get; }
        public string Description { get; }
        public string Id { get; }

        public void AddModifier(IModifier modifier);
    }
}
