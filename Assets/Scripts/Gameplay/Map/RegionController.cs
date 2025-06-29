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
        public string Id => Name.Replace(' ', '_').ToLower();
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
            IController.Lookup[Id] = this;
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
    }

}
