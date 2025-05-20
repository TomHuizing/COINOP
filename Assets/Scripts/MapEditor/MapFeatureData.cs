using System.Collections.Generic;
using UnityEngine;

public class MapFeatureData
{
    public long Id { get; private set; }
    public FeatureType Type { get; private set; }
    public string Name { get; private set; }
    public IEnumerable<Vector2> Vertices => vertices;

    private readonly List<Vector2> vertices = new();

    public MapFeatureData(long id, FeatureType type, string name, IEnumerable<Vector2> vertices)
    {
        Id = id;
        Type = type;
        Name = name;
        this.vertices.AddRange(vertices);
    }
}
