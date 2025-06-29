using System.Collections.Generic;
using System.Linq;
using Gameplay.Modifiers;
using UnityEngine;

namespace Gameplay.Map
{
    public class RegionModel
    {
        private readonly List<IRegionModifier> modifiers = new();
        public IEnumerable<IRegionModifier> Modifiers => modifiers.Where(m => !m.Expired);
        public IEnumerable<IRegionModifier> TransientModifiers => Modifiers.Where(m => m.Persistence == ModifierPersistence.Transient);
        public IEnumerable<IRegionModifier> SustainedModifiers => Modifiers.Where(m => m.Persistence == ModifierPersistence.Sustained);

        public RegionStats TransientSum => TransientModifiers.Select(m => m.Stats).Aggregate(RegionStats.Zero, (a, b) => a + b);
        public RegionStats SustainedSum => SustainedModifiers.Select(m => m.Stats).Aggregate(RegionStats.Zero, (a, b) => a + b);

        public readonly string Name;

        public Vector2 Center { get; private set; }
        public readonly List<RegionModel> Neighbors = new();

        public RegionStats BaseStats { get; private set; }
        public RegionStats CurrentStats => BaseStats + TransientSum;

        public RegionModel(string name, Vector2 center)
        {
            Name = name;
            Center = center;
            BaseStats = RegionStats.Random;
        }

        public void UpdateNeighbors(IEnumerable<RegionModel> neighbors)
        {
            Neighbors.Clear();
            Neighbors.AddRange(neighbors);
        }

        public void AddModifier(IRegionModifier modifier)
        {
            if (modifier == null || modifier.Expired)
                return;
            modifiers.Add(modifier);
        }

        public void Simulate()
        {
            modifiers.RemoveAll(m => m.Expired);
            BaseStats += SustainedSum;
        }
    }
}
