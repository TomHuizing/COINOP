using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickBlocker : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnityEvent onClick = new();

    public void OnPointerDown(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}
