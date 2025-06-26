using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Modifiers;

namespace Gameplay.Common
{
    public interface IController
    {
        public string Name { get; }
        public string Description { get; }

        // public IEnumerable<IModifier> Modifiers { get; }

        public void AddModifier(IModifier modifier);
        public bool RemoveModifier(IModifier modifier);

        public event Action OnChanged;
    }
}
