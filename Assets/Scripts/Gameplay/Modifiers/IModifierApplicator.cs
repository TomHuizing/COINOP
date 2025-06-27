using System;
using Gameplay.Common;

namespace Gameplay.Modifiers
{
    public interface IModifierApplicator<TSource>
        where TSource : IController
    {
        public TSource Source { get; }
        public bool TryApply(out IModifier modifier);
    }
}
