using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Gameplay.Time;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceManager", menuName = "Singletons/ResourceManager")]
public class ResourceManager : ScriptableObject
{
    private readonly List<IResourceModifier> modifiers = new();

    // Resource values
    [SerializeField] private int supplies = 100;
    [SerializeField] private int intel = 100;
    [SerializeField] private int influence = 100;

    [SerializeField] private GameClock clock;

    public event Action<int, int, int> OnResourceChanged;

    public int Supplies => supplies;
    public int Intel => intel;
    public int Influence => influence;

    public int SuppliesChange => modifiers.Select(m => m.Supplies).Sum();
    public int IntelChange => modifiers.Select(m => m.Intel).Sum();
    public int InfluenceChange => modifiers.Select(m => m.Influence).Sum();

    void OnEnable()
    {
        if(clock != null)
            clock.OnCycle += ModifyValues;
    }

    void OnDisable()
    {
        if(clock != null)
            clock.OnCycle -= ModifyValues;
    }

    public void AddResources(int supplies, int intel, int influence)
    {
        this.supplies += supplies;
        this.intel += intel;
        this.influence += influence;
        OnResourceChanged?.Invoke(supplies, intel, influence);
    }

    public void AddModifier(IResourceModifier modifier, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return;
        cancellationToken.Register(() => modifiers.Remove(modifier));
        modifiers.Add(modifier);
    }

    private void ModifyValues(DateTime now, TimeSpan delta)
    {
        supplies += SuppliesChange;
        intel += IntelChange;
        influence += InfluenceChange;
        OnResourceChanged?.Invoke(supplies, intel, influence);
    }
}
