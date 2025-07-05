using System.Collections.Generic;

namespace UI.Interaction
{
    public interface ILinkable : IInteractable
    {
        private static readonly Dictionary<string, ILinkable> linkables = new();

        protected static void RegisterLinkable(ILinkable linkable) { linkables.Add(linkable.Id, linkable); }
        protected static void DeregisterLinkable(ILinkable linkable) { linkables.Remove(linkable.Id); }
        public static ILinkable GetLinkable(string Id) => linkables[Id];

        public string Id { get; }
        public string Link { get; }
    }
}
