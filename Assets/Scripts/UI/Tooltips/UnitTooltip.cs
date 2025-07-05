using Gameplay.Units;
using UI.Elements;
using UI.Interaction;
using UnityEngine;

namespace UI.Tooltips
{
    public class UnitTooltip : MonoBehaviour, ITooltip
    {
        public UnitController Controller { get; set; }

        public ITooltippable Source { get; set; }

        public RectTransform RectTransform => GetComponent<RectTransform>();

        void Start()
        {
            gameObject.SetActive(false);
        }

        public void Show() => gameObject.SetActive(true);
        public void Destroy() => Destroy(this);
    }
}
