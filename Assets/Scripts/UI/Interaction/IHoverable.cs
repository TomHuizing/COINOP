using System;

namespace UI.Interaction
{
    public interface IHoverable : IInteractable
    {
        public bool IsHovered { get; }

        public event Action<IHoverable> OnHover;
        public event Action<IHoverable> OnUnhover;

        public void Hover();
        public void Unhover();
    }
}
