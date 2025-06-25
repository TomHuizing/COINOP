using System.Collections;
using UnityEngine;
using TMPro;

namespace UI.Elements
{
    public class Tooltip : MonoBehaviour
    {
        static public Tooltip Instance { get; private set; }

        public enum Anchor
        {
            TopLeft,
            TopMid,
            TopRight,
            MidRight,
            BottomRight,
            BottomMid,
            BottomLeft,
            MidLeft
        }

        [SerializeField] GameObject view;
        [SerializeField] TextMeshProUGUI description;
        [SerializeField] Anchor anchor = Anchor.BottomLeft;
        [SerializeField] Vector2 offset = new(10, 10);
        [SerializeField] float delay = 0.5f;

        private RectTransform rectTransform;
        private Coroutine delayedShowCoroutine;
        // private GameObject view;
         
        public string DescriptionValue
        {
            get => description != null ? description.text : "";
            set
            {
                if (description != null)
                {
                    description.text = value;
                    UpdateHeight();
                }
            }
        }

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            // view = GetComponentInChildren<RectTransform>().gameObject;
            view.SetActive(false);
        }

        void Update()
        {
            if(view.activeSelf)
                UpdatePosition(Input.mousePosition);
        }

        public void Show()
        {
            if (delay > 0)
            {
                Hide();
                delayedShowCoroutine = StartCoroutine(DelayedShowTooltip(delay));
            }
            else
                ImmediateShowTooltip();
        }
        
        private IEnumerator DelayedShowTooltip(float delay)
        {
            yield return new WaitForSeconds(delay);
            ImmediateShowTooltip();
        }

        private void ImmediateShowTooltip()
        {
            if (delayedShowCoroutine != null)
                StopCoroutine(delayedShowCoroutine);
            UpdatePosition(Input.mousePosition);
            view.SetActive(true);
        }

        public void Hide()
        {
            if (delayedShowCoroutine != null)
                StopCoroutine(delayedShowCoroutine);
            view.SetActive(false);
        }

        // [UnityEngine.ContextMenu("Update Height")]
        public void UpdateHeight()
        {
            if(description != null)
            {
                RectTransform selfRect = GetComponent<RectTransform>();
                selfRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, description.preferredHeight);
            }
        }

        public void UpdatePosition(Vector2 position)
        {
            rectTransform.pivot = anchor switch
            {
                Anchor.TopLeft => new(0,1),
                Anchor.TopMid => new(0.5f, 1),
                Anchor.TopRight => new(1, 1),
                Anchor.MidRight => new(1, 0.5f),
                Anchor.BottomRight => new(1, 0),
                Anchor.BottomMid => new(0.5f, 0),
                Anchor.BottomLeft => new(0, 0),
                Anchor.MidLeft => new(0, 0.5f),
                _ => Vector2.zero
            };
            Vector2 actualOffset = offset * ((rectTransform.pivot - new Vector2(0.5f, 0.5f)) * -2);
            rectTransform.position = position + actualOffset;
        }
    }
}