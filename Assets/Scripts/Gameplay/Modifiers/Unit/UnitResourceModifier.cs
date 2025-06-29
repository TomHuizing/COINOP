using System;
using Gameplay.Resources;
using Gameplay.Units;

namespace Gameplay.Modifiers.Unit
{
    public class UnitResourceModifier : IResourceModifier<UnitController>
    {
        public ResourcePackage Resources { get; } = new(-1, 0, 0);

        public UnitController Source { get; private set; }

        public ResourceManager Target { get; } = ResourceManager.instance;

        public string SourceName => Source.Name;

        public string TargetName => Target.Name;

        public string Description => $"Costs to operate {SourceName}";

        public string ModifierValues => Resources.ToString();

        public ModifierPersistence Persistence { get; } = ModifierPersistence.Sustained;

        public DateTime TimeOfExpiry => DateTime.MinValue;

        public bool Expired => Source == null || Source.enabled == false;

        public UnitResourceModifier(UnitController source)
        {
            Source = source;
        }
    }
}
