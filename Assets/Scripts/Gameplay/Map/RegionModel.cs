using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Map
{
    public class RegionModel
    {
        public readonly string Name;

        public Vector2 Center { get; private set; }
        public readonly List<RegionModel> Neighbors = new();

        public RegionStats Stats { get; private set; }

        public RegionModel(string name, Vector2 center)
        {
            Name = name;
            Center = center;
            Stats = RegionStats.Random;
        }

        public void UpdateNeighbors(IEnumerable<RegionModel> neighbors)
        {
            Neighbors.Clear();
            Neighbors.AddRange(neighbors);
        }

        public void AddStats(RegionStats stats)
        {
            Stats += stats;
        }
    }
}
