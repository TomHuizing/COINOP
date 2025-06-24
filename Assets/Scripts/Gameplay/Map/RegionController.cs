using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Map
{
    public class RegionController : MonoBehaviour
    {
        [SerializeField] private List<RegionController> neighbors = new();

        private RegionModel model;

        public string Name => model.Name;
        public float Control => model.Control;
        public float Support => model.Support;
        public float Infra => model.Infra;
        public IEnumerable<RegionController> Neighbors => neighbors;

        void Awake()
        {
            model = new(name, transform.position);
        }

        void Start()
        {
            model.UpdateNeighbors(Neighbors.Select(x => x.model));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
