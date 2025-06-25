using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    [ExecuteAlways]
    public class ContextMenu : MonoBehaviour, IPointerDownHandler
    {
        static public ContextMenu Instance { get; private set; }

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

        [SerializeField] private GameObject clickBlocker;
        [SerializeField] private ContextMenuItem contextMenuItemPrefab;
        [SerializeField] private Anchor origin = Anchor.TopLeft;
        [SerializeField] private Vector2 offset = new(10, 10);
        // [SerializeField] private RectOffset padding = new(10, 10, 10, 10);
        
        // Origin origin = Origin.RightBottom;

        private readonly List<ContextMenuItem> menuItems = new();
        private RectTransform rectTransform;


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
            rectTransform = GetComponent<RectTransform>();
            Hide();
        }

        public void Show(IEnumerable<NamedAction> items) => Show(items, Input.mousePosition);

        public void Show(IEnumerable<NamedAction> items, Vector2 position)
        {
            clickBlocker.SetActive(true);
            gameObject.SetActive(true);
            CreateMenuItems(items);

            rectTransform.pivot = origin switch
            {
                Anchor.TopLeft => new(0, 1),
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

        private void CreateMenuItems(IEnumerable<NamedAction> items)
        {
            foreach(var item in items)
            {
                ContextMenuItem menuItem = Instantiate(contextMenuItemPrefab);
                menuItem.Init(item.Name, item.Enabled());
                menuItem.OnClick += () =>
                {
                    Hide();
                    item.Invoke();
                };
                menuItem.transform.SetParent(transform);
                menuItems.Add(menuItem);
            }
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            Hide();
        }


        public void Hide()
        {

            foreach (var g in menuItems)
            {
                Destroy(g.gameObject);
            }
            menuItems.Clear();
            clickBlocker.SetActive(false);
            gameObject.SetActive(false);
        }   

        public bool IsShowing()
        {
            return gameObject.activeSelf;
        }
    }
}