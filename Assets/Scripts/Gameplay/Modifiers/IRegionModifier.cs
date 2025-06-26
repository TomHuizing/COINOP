using System;
using Gameplay.Common;
using Gameplay.Map;

namespace Gameplay.Modifiers
{
    public interface IRegionModifier<TSource> : IRegionModifier, IModifier<TSource, RegionController> where TSource : IController
    {
        
    }

    public interface IRegionModifier : IModifier
    {
        public RegionStats Stats { get; }
    }
}
