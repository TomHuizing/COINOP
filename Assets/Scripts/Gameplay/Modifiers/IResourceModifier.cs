using Gameplay.Common;
using Gameplay.Resources;

namespace Gameplay.Modifiers
{
    public interface IResourceModifier<TSource> : IResourceModifier, IModifier<TSource, ResourceManager> where TSource : IController
    {

    }

    public interface IResourceModifier : IModifier
    {
        public ResourcePackage Resources { get; }
    }
}
