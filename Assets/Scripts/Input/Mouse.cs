using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputSystem
{
    public class Mouse
    {
        public Vector2 ScreenPosition => Input.mousePosition;
        public Vector2 WorldPosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
        public Vector2 PreviousScreenPosition => Input.mousePosition - Input.mousePositionDelta;
        public Vector2 PreviousWorldPosition => Camera.main.ScreenToWorldPoint(PreviousScreenPosition);
        public Vector2 ScreenDelta => (Vector2)Input.mousePositionDelta;
        public Vector2 WorldDelta => WorldPosition - PreviousWorldPosition;
        public IEnumerable<RaycastHit2D> RaycastHits => raycastHits;
        public bool IsOverUi => EventSystem.current.IsPointerOverGameObject();

        public event Action<Vector2, RaycastHit2D[]> OnHover;
        public event Action<MouseButton, Vector2, RaycastHit2D[]> OnButtonDown;
        public event Action<MouseButton, Vector2, RaycastHit2D[]> OnButtonHeld;
        public event Action<MouseButton, Vector2, RaycastHit2D[]> OnButtonUp;
        public event Action<MouseButton, Vector2, RaycastHit2D[]> OnClick;
        public event Action<float> OnScroll;
        public event Action<Vector2, Vector2> OnDrag;
        public event Action<Vector2, Vector2> OnEndDrag;

        private RaycastHit2D[] raycastHits = new RaycastHit2D[0];
        private readonly Dictionary<MouseButton, Vector2> clickStart = new();

        public Mouse(MonoBehaviour monoBehaviour)
        {
            monoBehaviour.StartCoroutine(MouseUpdate());
        }

        public IEnumerator MouseUpdate()
        {
            while (true)
            {
                raycastHits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
                if (IsOverUi)
                {
                    yield return null; //TODO: Implement a way to end drags and clicks over the UI that started on the world
                    continue;
                }
                OnHover?.Invoke(WorldPosition, raycastHits);
                foreach (MouseButton button in GetButtonDown())
                {
                    OnButtonDown?.Invoke(button, WorldPosition, raycastHits);
                    clickStart[button] = WorldPosition;
                }
                foreach (MouseButton button in GetButtonUp())
                {
                    OnButtonUp?.Invoke(button, WorldPosition, raycastHits);
                    if (clickStart.TryGetValue(button, out Vector2 start))
                    {
                        if (start == WorldPosition)
                            OnClick?.Invoke(button, WorldPosition, raycastHits);
                        else
                            OnEndDrag?.Invoke(start, WorldPosition);
                        clickStart.Remove(button);
                    }

                }
                foreach (MouseButton button in GetButtonHeld())
                {
                    OnButtonHeld?.Invoke(button, WorldPosition, raycastHits);
                    if (clickStart.TryGetValue(button, out Vector2 start))
                    {
                        if (start != WorldPosition)
                            OnDrag?.Invoke(start, WorldPosition);
                    }
                }
                if (Input.mouseScrollDelta != Vector2.zero)
                {
                    OnScroll?.Invoke(Input.mouseScrollDelta.y);
                }
                yield return null;
            }
        }

        public MouseButton[] GetButtonDown()
        {
            List<MouseButton> buttons = new();
            if (Input.GetMouseButtonDown(0)) buttons.Add(MouseButton.Left);
            if (Input.GetMouseButtonDown(1)) buttons.Add(MouseButton.Right);
            if (Input.GetMouseButtonDown(2)) buttons.Add(MouseButton.Middle);
            return buttons.ToArray();
        }

        public MouseButton[] GetButtonUp()
        {
            List<MouseButton> buttons = new();
            if (Input.GetMouseButtonUp(0)) buttons.Add(MouseButton.Left);
            if (Input.GetMouseButtonUp(1)) buttons.Add(MouseButton.Right);
            if (Input.GetMouseButtonUp(2)) buttons.Add(MouseButton.Middle);
            return buttons.ToArray();
        }

        public MouseButton[] GetButtonHeld()
        {
            List<MouseButton> buttons = new();
            if (Input.GetMouseButton(0)) buttons.Add(MouseButton.Left);
            if (Input.GetMouseButton(1)) buttons.Add(MouseButton.Right);
            if (Input.GetMouseButton(2)) buttons.Add(MouseButton.Middle);
            return buttons.ToArray();
        }
    }
}
