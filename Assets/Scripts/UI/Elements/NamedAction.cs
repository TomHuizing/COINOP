using System;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Elements
{
    public class NamedAction
    {

        public string Name { get; set; }
        public event Action OnInvoke;

        public NamedAction(string name, Action onInvoke)
        {
            Name = name;
            OnInvoke += onInvoke;
        }

        public void Invoke() => OnInvoke?.Invoke();
    }
}
