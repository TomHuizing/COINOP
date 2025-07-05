using System;
using UI.Elements;

namespace UI.Interaction
{
    public interface ITooltippable : IInteractable
    {
        public ITooltip CreateTooltip();
    }
}
