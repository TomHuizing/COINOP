using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : MonoBehaviour, ITimeReceiver
{
    public static ResourceManager Instance { get; private set; }


    // Resource values
    [SerializeField] private int supplies = 100;
    [SerializeField] private int intel = 100;
    [SerializeField] private int influence = 100;

    [SerializeField] private UnityEvent<int, int, int> OnResourcesChanged = new();

    private readonly List<IPeriodicResourceChanger> resourceChangers = new();

    public int Supplies => supplies;
    public int Intel => intel;
    public int Influence => influence;
    public IEnumerable<IPeriodicResourceChanger> ResourceChangers => resourceChangers;
    public IEnumerable<IPeriodicResourceChanger> SuppliesChangers => resourceChangers.Where(x => x.Supplies != 0);
    public IEnumerable<IPeriodicResourceChanger> IntelChangers => resourceChangers.Where(x => x.Intel != 0);
    public IEnumerable<IPeriodicResourceChanger> InfluenceChangers => resourceChangers.Where(x => x.Influence != 0);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeManager.Instance.RegisterReceiver(this); // Register this object to receive time updates
        OnResourcesChanged.Invoke(supplies, intel, influence); // Invoke the resources changed event with the initial values
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TryGetResources(int supplies, int intel, int influence)
    {
        if (supplies > this.supplies || intel > this.intel || influence > this.influence)
            return false;

        return true;
    }

    public bool TrySpendResources(int supplies, int intel, int influence)
    {
        if(!TryGetResources(supplies, intel, influence))
            return false;

        ChangeResources(-supplies, -intel, -influence);
        return true;
    }

    public void ChangeResources(int supplies, int intel, int influence)
    {
        this.supplies += supplies;
        this.intel += intel;
        this.influence += influence;
        OnResourcesChanged.Invoke(this.supplies, this.intel, this.influence);
    }

    public void RegisterPeriodicResourceChanger(IPeriodicResourceChanger resourceChanger)
    {
        resourceChangers.Add(resourceChanger);
    }

    public void Cycle(CyclePeriod period, DateTime dateTime)
    {
        foreach (IPeriodicResourceChanger resourceChanger in resourceChangers)
        {
            if(resourceChanger == null)
                resourceChangers.Remove(resourceChanger);
            else
                ChangeResources(resourceChanger.Supplies, resourceChanger.Intel, resourceChanger.Influence);
        }
        if(resourceChangers.Count > 0)
            OnResourcesChanged.Invoke(supplies, intel, influence);
    }

    public void Tick(TickPeriod period, DateTime dateTime) {}
    public void Day(DateTime dateTime) {}
    public void TimeStart() {}
    public void TimeStop() {}
}
