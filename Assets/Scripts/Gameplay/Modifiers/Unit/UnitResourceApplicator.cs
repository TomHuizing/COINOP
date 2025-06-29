using System;
using Gameplay.Units;

namespace Gameplay.Modifiers.Unit
{
    public class UnitResourceApplicator : IModifierApplicator<UnitController>
    {
        public UnitController Source { get; private set; }

        public event Action<IModifier> OnModifier;

        public UnitResourceApplicator(UnitController source)
        {
            Source = source != null ? source : throw new ArgumentNullException(nameof(source));
            source.OnCreated += Create;
        }

        private void Create()
        {
            ResourceManager.instance.AddModifier(new UnitResourceModifier(Source));
        }
    }
}
