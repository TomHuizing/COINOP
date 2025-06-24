using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Elements
{
    [Serializable]
    public class NamedAction
    {

        [SerializeField] private UnityEvent OnInvokeEvent;
        [SerializeField] private string text;

        public string Text { get => text; set => text = value; }
        public event Action OnInvoke;


        public void Invoke()
        {
            OnInvokeEvent?.Invoke();
            OnInvoke?.Invoke();
        }
    }
}
