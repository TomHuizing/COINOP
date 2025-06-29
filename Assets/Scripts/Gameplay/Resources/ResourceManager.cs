using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Common;
using Gameplay.Modifiers;
using Gameplay.Resources;
using Gameplay.Time;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceManager", menuName = "Singletons/ResourceManager")]
public class ResourceManager : ScriptableSingleton<ResourceManager>, IController
{
    private ResourcePackage baseResources = new(100, 100, 100);

    private readonly List<IResourceModifier> modifiers = new();

    [SerializeField] private GameClock clock;

    public string Name { get; } = "Resource Manager";
    public string Description { get; } = "The resource manager.";
    public string Id { get; } = "resource_manager";

    public event Action<int, int, int> OnResourceChanged;

    public ResourcePackage Resources => baseResources + TransientSum;

    public float Supplies => baseResources.Supplies;
    public float Intel => baseResources.Intel;
    public float Influence => baseResources.Influence;

    public IEnumerable<IResourceModifier> Modifiers => modifiers.Where(m => !m.Expired);
    public IEnumerable<IResourceModifier> TransientModifiers => Modifiers.Where(m => m.Persistence == ModifierPersistence.Transient);
    public IEnumerable<IResourceModifier> SustainedModifiers => Modifiers.Where(m => m.Persistence == ModifierPersistence.Sustained);

    public ResourcePackage TransientSum => TransientModifiers.Select(m => m.Resources).Aggregate(ResourcePackage.Zero, (a, b) => a + b);
    public ResourcePackage SustainedSum => SustainedModifiers.Select(m => m.Resources).Aggregate(ResourcePackage.Zero, (a, b) => a + b);

    void Awake()
    {
    }

    void OnEnable()
    {
        IController.Lookup[Id] = this;
        GameClock.instance.OnCycle += (_,_) => Simulate();
        
    }

    public void AddModifier(IResourceModifier modifier)
    {
        modifiers.Add(modifier);
    }

    public void AddModifier(IModifier modifier)
    {
        if (modifier is IResourceModifier resourceModifier)
            AddModifier(resourceModifier);
        else
            Debug.LogError($"Modifier {modifier.GetType().Name} is not a valid resource modifier");
    }

    public void Simulate()
    {
        modifiers.RemoveAll(m => m.Expired);
        baseResources += SustainedSum;
    }
}
