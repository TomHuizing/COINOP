using System;

namespace UI.Interaction
{
    public interface ITooltippable : IInteractable
    {
        public string TooltipText { get; }
    }
}
