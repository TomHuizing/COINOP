using System.Collections.Generic;
using UnityEngine;

public class RoadData
{
    public long Id { get; private set; }
    public string Name { get; private set; } = "Unnamed Road";
    public IEnumerable<Vector2> Vertices => vertices;

    private readonly List<Vector2> vertices = new();

    public RoadData(IEnumerable<Vector2> vertices)
    {
        this.vertices.AddRange(vertices);
    }

    public void AddSegment(IEnumerable<Vector2> segment, bool end = true)
    {
        if(end)
            vertices.AddRange(segment);
        else
            vertices.InsertRange(0, segment);
    }
}
