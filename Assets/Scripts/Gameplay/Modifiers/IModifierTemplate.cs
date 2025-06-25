using System;
using Gameplay.Common;

namespace Gameplay.Modifiers
{
    public interface IModifierTemplate<TSource, TTarget> where TSource : IController where TTarget : IController
    {
        public bool ConditionsMet(TSource source, TTarget target);
        public IModifier<TSource, TTarget> Apply(TSource source, TTarget target);
    }
}
