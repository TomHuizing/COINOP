using System.Collections.Generic;
using UnityEngine;

public class RegionModel
{
    public readonly string Name;

    public Vector2 Center { get; private set; }
    public readonly List<RegionModel> Neighbors = new();

    public float Support { get; private set; }
    public float Control { get; private set; }
    public float Infra { get; private set; }

    public RegionModel(string name, Vector2 center)
    {
        Name = name;
        Center = center;

        Support = Random.Range(0f, 1f);
        Control = Random.Range(0f, 1f);
        Infra = Random.Range(0f, 1f);
    }

    public void UpdateNeighbors(IEnumerable<RegionModel> neighbors)
    {
        Neighbors.Clear();
        Neighbors.AddRange(neighbors);
    }
}
