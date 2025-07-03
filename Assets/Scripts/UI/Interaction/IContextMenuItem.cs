using System;
using Gameplay.Common;

namespace UI.Interaction
{
    public interface IContextMenuItem
    {
        public string Text { get; }
        public bool Enabled { get; }
        public bool Shown { get; }
        public void Select();
    }
}
