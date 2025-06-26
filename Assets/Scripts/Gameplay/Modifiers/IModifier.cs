using System;
using Gameplay.Common;

namespace Gameplay.Modifiers
{
    public interface IModifier
    {
        public string SourceName { get; }
        public string TargetName { get; }
        public string Description { get; }
        public string ModifierValues { get; }
    }

    public interface IModifier<TSource, TTarget> : IModifier where TSource : IController where TTarget : IController
    {
        public TSource Source { get; }
        public TTarget Target { get; }

        

        //public bool IsStackable { get; }
    }
}
