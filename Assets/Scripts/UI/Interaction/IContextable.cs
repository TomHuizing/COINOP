using System.Collections.Generic;
using Gameplay.Common;
using UI.Elements;

namespace UI.Interaction
{
    public interface IContextable : IInteractable
    {
        public void ContextClick(IController context) { }
        public void ContextAlt(IController context) { }
        public IEnumerable<IContextMenuItem> GetContextMenu(IController context);
    }
}
