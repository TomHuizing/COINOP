using System;

namespace UI.Interaction
{
    public interface IClickable : IInteractable
    {
        public void Click(KeyModifiers modifiers);
    }
}
