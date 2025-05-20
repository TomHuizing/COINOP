using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MapLine : MonoBehaviour
{
    private readonly List<Vector2> vertices = new();

    public IEnumerable<Vector2> Vertices => vertices;
    public float Width
    {
        get => GetComponent<LineRenderer>().startWidth;
        set
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = value;
            lineRenderer.endWidth = value;
        }
    }

    public void SetVertices(IEnumerable<Vector2> newVertices)
    {
        vertices.Clear();
        vertices.AddRange(newVertices);
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = vertices.Count;
        lineRenderer.SetPositions(vertices.Select(v => (Vector3)v).ToArray());
    }
}
