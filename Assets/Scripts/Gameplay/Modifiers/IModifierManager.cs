using System.Collections.Generic;
using Gameplay.Common;

namespace Gameplay.Modifiers
{
    public interface IModifierManager<TSource> where TSource : IController
    {
        public TSource Source { get; }

        public IEnumerable<IModifierApplicator<TSource>> Applicators { get; }

        public void AddApplicator(IModifierApplicator<TSource> applicator);
        public bool RemoveApplicator(IModifierApplicator<TSource> applicator);
    }
}
