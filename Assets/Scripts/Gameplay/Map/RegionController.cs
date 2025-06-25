using System.Collections.Generic;
using System.Linq;
using Gameplay.Common;
using Gameplay.Modifiers;
using UnityEngine;

namespace Gameplay.Map
{
    public class RegionController : MonoBehaviour, IController
    {
        [SerializeField] private List<RegionController> neighbors = new();

        private RegionModel model;

        public string Name => model.Name;
        public string Description => $"Region: {model.Name}";
        // public float Control => model.Control;
        // public float Support => model.Support;
        // public float Infra => model.Infra;
        public RegionStats Stats => model.Stats;
        public IEnumerable<RegionController> Neighbors => neighbors;


        void Awake()
        {
            model = new(name, transform.position);
        }

        void Start()
        {
            model.UpdateNeighbors(Neighbors.Select(x => x.model));
        }

        public void AddStats(RegionStats stats)
        {
            model.AddStats(stats);
        }

        public void AddModifier(IModifier modifier)
        {
            throw new System.NotImplementedException();
        }
    }

}
