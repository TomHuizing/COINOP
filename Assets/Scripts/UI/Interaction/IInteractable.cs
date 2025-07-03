using System;
using Gameplay.Common;

namespace UI.Interaction
{
    public interface IInteractable
    {
        public string Name => Controller?.Name ?? "UNNAMED CONTROLLER";
        public IController Controller { get; }

        public InteractionLayer Layer { get; }
    }
}
