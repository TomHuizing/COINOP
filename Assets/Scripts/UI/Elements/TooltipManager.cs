using System.Collections;
using UI.Interaction;
using UnityEngine;

namespace UI.Elements
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance { get; private set; }

        [SerializeField] Canvas parent;
        [SerializeField] ElementAnchor anchor = ElementAnchor.BottomLeft;
        [SerializeField] Vector2 offset = new(10, 10);
        [SerializeField] float delay = 0.5f;

        private Vector2 Pivot => anchor switch
        {
            ElementAnchor.TopLeft => new(0, 0),
            ElementAnchor.TopMid => new(0.5f, 0),
            ElementAnchor.TopRight => new(1, 0),
            ElementAnchor.MidRight => new(1, 0.5f),
            ElementAnchor.BottomRight => new(1, 1),
            ElementAnchor.BottomMid => new(0.5f, 1),
            ElementAnchor.BottomLeft => new(0, 1),
            ElementAnchor.MidLeft => new(0, 0.5f),
            _ => new(0,0),
        };

        private Vector2 ActualOffset => anchor switch
        {
            ElementAnchor.TopLeft => new(-1 * offset.x, -1 * offset.y),
            ElementAnchor.TopMid => new(0, -1 * offset.y),
            ElementAnchor.TopRight => new(offset.x, -1 * offset.y),
            ElementAnchor.MidRight => new(offset.x, 0),
            ElementAnchor.BottomRight => new(offset.x, offset.y),
            ElementAnchor.BottomMid => new(0, offset.y),
            ElementAnchor.BottomLeft => new(-1 * offset.x, offset.y),
            ElementAnchor.MidLeft => new(-1 * offset.x, 0),
            _ => new(-1 * offset.x, -1 * offset.y)
        };

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public ITooltip ShowTooltip(ITooltippable tooltippable)
        {
            ITooltip tooltip = tooltippable.CreateTooltip();
            tooltip.RectTransform.SetParent(parent.transform);
            tooltip.RectTransform.pivot = Pivot;
            tooltip.RectTransform.position = (Vector2)Input.mousePosition + ActualOffset;
            if (delay <= 0)
                tooltip.Show();
            else
                StartCoroutine(DelayedShow(tooltip, delay));
            return tooltip;
        }

        IEnumerator DelayedShow(ITooltip tooltip, float delay)
        {
            if (tooltip == null)
                yield break;
            yield return new WaitForSeconds(delay);
            if (tooltip != null)
                tooltip.Show();
        }

    }
}
