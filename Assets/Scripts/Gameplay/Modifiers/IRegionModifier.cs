using System;
using Gameplay.Common;
using Gameplay.Map;

namespace Gameplay.Modifiers
{
    public interface IRegionModifier<TSource> : IModifier<TSource, RegionController> where TSource : IController
    {
        public RegionStats StatModifiers { get; }
    }
}
