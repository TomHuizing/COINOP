using System;
using UnityEngine;

[Serializable]
public class UnitType
{
    [SerializeField] private string id;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private string icon;

    [SerializeField] private float speed;
    [SerializeField] private int strength;

    [SerializeField] private ResourcePackage buildCost;
    [SerializeField] private ResourcePackage staticUpkeep;
    [SerializeField] private ResourcePackage missionUpkeep;
    [SerializeField] private ResourcePackage maxInventory;

    private Sprite iconSprite;

    public string Id => id;
    public string Name => name;
    public string Description => description;
    public Sprite Icon => iconSprite = iconSprite != null ? iconSprite : Resources.Load<Sprite>(icon);

    public float Speed => speed;
    public int Strength => strength;

    public ResourcePackage BuildCost => buildCost;
    public ResourcePackage StaticUpkeep => staticUpkeep;
    public ResourcePackage MissionUpkeep => missionUpkeep;
    public ResourcePackage MaxInventory => maxInventory;
}
