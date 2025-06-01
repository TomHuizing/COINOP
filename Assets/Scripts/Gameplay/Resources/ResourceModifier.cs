using System;
using System.Threading;
using UnityEngine;

public class ResourceModifier : MonoBehaviour, IResourceModifier
{
    private CancellationTokenSource cts;

    [SerializeField] private string description;
    [SerializeField] private int supplies;
    [SerializeField] private int intel;
    [SerializeField] private int influence;
    [SerializeField] private ResourceManager resourceManager;

    public string Name => name;
    public string Description => description;
    public int Supplies => supplies;
    public int Intel => intel;
    public int Influence => influence;

    void OnEnable()
    {
        cts = new();
        resourceManager.AddModifier(this, cts.Token);
    }

    void OnDisable()
    {
        cts.Cancel();
    }
}
