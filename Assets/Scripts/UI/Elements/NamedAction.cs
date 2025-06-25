using System;

namespace UI.Elements
{
    public class NamedAction
    {

        public string Name => string.IsNullOrEmpty(getName?.Invoke()) ? "Unnamed Action" : getName.Invoke();
        public readonly Func<bool> Enabled;
        public readonly Func<bool> Shown;
        public event Action OnInvoke;

        private readonly Func<string> getName;

        public NamedAction(string name, Action onInvoke, Func<bool> enabled = null, Func<bool> shown = null)
        {
            getName = () => string.IsNullOrEmpty(name) ? "Unnamed Action" : name;
            OnInvoke += onInvoke;
            Enabled = enabled ?? (() => true);
            Shown = shown ?? (() => true);
        }

        public NamedAction(Func<string> name, Action onInvoke, Func<bool> enabled = null, Func<bool> shown = null)
        {
            getName = name;
            OnInvoke += onInvoke;
            Enabled = enabled ?? (() => true);
            Shown = shown ?? (() => true);
        }

        public void Invoke() => OnInvoke?.Invoke();
    }
}
