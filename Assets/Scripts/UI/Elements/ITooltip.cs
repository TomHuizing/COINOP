using UI.Interaction;
using UnityEngine;

namespace UI.Elements
{
    public interface ITooltip
    {
        public RectTransform RectTransform { get; }

        public ITooltippable Source { get; }
        public void Show();
        public void Destroy();
    }
}
