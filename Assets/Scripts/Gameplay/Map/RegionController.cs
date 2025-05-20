using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegionController : MonoBehaviour
{
    
    [SerializeField] private List<RegionController> neighbors = new();

    private RegionModel model;
    private List<UnitController> units = new();

    public string Name => model.Name;
    public float Control => model.Control;
    public float Support => model.Support;
    public float Infra => model.Infra;
    public IEnumerable<RegionController> Neighbors => neighbors;
    public IEnumerable<UnitController> Units => units;

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

    public void RegisterPeriodicInfluence(IPeriodicRegionInfluence influence)
    {
        
    }
}

