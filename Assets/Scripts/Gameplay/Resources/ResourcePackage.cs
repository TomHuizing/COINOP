using System;
using UnityEngine;

namespace Gameplay.Resources
{
    public readonly struct ResourcePackage
    {
        public readonly int Supplies;
        public readonly int Intel;
        public readonly int Influence;

        public ResourcePackage(int supplies, int intel, int influence)
        {
            Supplies = supplies;
            Intel = intel;
            Influence = influence;
        }

        public ResourcePackage SetSupplies(int supplies) => new(supplies, Intel, Influence);
        public ResourcePackage SetIntel(int intel) => new(Supplies, intel, Influence);
        public ResourcePackage SetInlfluence(int influence) => new(Supplies, Intel, influence);

        public ResourcePackage AddSupplies(int supplies) => new(Supplies + supplies, Intel, Influence);
        public ResourcePackage AddIntel(int intel) => new(Supplies, Intel + intel, Influence);
        public ResourcePackage AddInfluence(int influence) => new(Supplies, Intel, Influence + influence);

        public ResourcePackage Clamp(ResourcePackage min, ResourcePackage max) => new
        (
            Math.Clamp(Supplies, min.Supplies, max.Supplies),
            Math.Clamp(Intel, min.Intel, max.Intel),
            Math.Clamp(Influence, min.Influence, max.Influence)
        );

        public ResourcePackage Lerp(ResourcePackage target, float t) => new
        (
            (int)Mathf.Lerp(Supplies, target.Supplies, t),
            (int)Mathf.Lerp(Intel, target.Intel, t),
            (int)Mathf.Lerp(Influence, target.Influence, t)
        );

        public ResourcePackage Decay(float t) => Lerp(Zero, t);

        public static ResourcePackage Zero => new(0, 0, 0);
        public static ResourcePackage One => new(1, 1, 1);

        public static ResourcePackage operator +(ResourcePackage left, ResourcePackage right) => new
        (
            left.Supplies + right.Supplies,
            left.Intel + right.Intel,
            left.Influence + right.Influence
        );

        public static ResourcePackage operator -(ResourcePackage left, ResourcePackage right) => new
        (
            left.Supplies - right.Supplies,
            left.Intel - right.Intel,
            left.Influence - right.Influence
        );
    }
}
