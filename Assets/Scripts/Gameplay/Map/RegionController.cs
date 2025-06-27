using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Common;
using Gameplay.Modifiers;
using Gameplay.Time;
using UnityEngine;

namespace Gameplay.Map
{
    public class RegionController : MonoBehaviour, IController
    {
        [SerializeField] private List<RegionController> neighbors = new();

        private RegionModel model;

        // private readonly List<IRegionModifier> modifiers = new();

        public string Name => model.Name;
        public string Description => $"Region: {model.Name}";
        public RegionStats Stats => model.CurrentStats;
        public IEnumerable<RegionController> Neighbors => neighbors;
        public IEnumerable<IRegionModifier> Modifiers => model.Modifiers;

        public event Action OnChanged;

        void Awake()
        {
            model = new(name, transform.position);
        }

        void Start()
        {
            model.UpdateNeighbors(Neighbors.Select(x => x.model));
            GameClock.instance.OnCycle += (_,_) => model.Simulate();
        }

        // public void AddStats(RegionStats stats) => model.AddStats(stats);

        public void AddModifier(IModifier modifier)
        {
            if (modifier is IRegionModifier regionModifier)
                AddModifier(regionModifier);
            else
                Debug.LogError($"Modifier {modifier.GetType().Name} is not a valid modifier for region {name}.");
        }

        public void AddModifier(IRegionModifier modifier) => model.AddModifier(modifier);

        // public bool RemoveModifier(IModifier modifier)
        // {
        //     if (modifier is IRegionModifier regionModifier)
        //     {
        //         return RemoveModifier(regionModifier);
        //     }
        //     else
        //     {
        //         Debug.LogError($"Modifier {modifier.GetType().Name} is not a valid modifier for region {name}.");
        //         return false;
        //     }
        // }

        // public bool RemoveModifier(IRegionModifier modifier) => modifiers.Remove(modifier);

        // public void Simulate(DateTime now, TimeSpan delta)
        // {
        //     AddStats(modifiers
        //         .Where(m => m.Persistence == ModifierPersistence.Sustained)
        //         .Select(m => m.Stats)
        //         .Aggregate((a, b) => a + b));
        // }
    }

}
