using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MapPolygon : MonoBehaviour
{
    private readonly List<Vector2> vertices = new();

    public IEnumerable<Vector2> Vertices => vertices;

    public void SetVertices(IEnumerable<Vector2> newVertices)
    {
        vertices.Clear();
        vertices.AddRange(newVertices);
        vertices.Reverse();
        
        
        MeshFilter filter = GetComponent<MeshFilter>();
        Triangulator triangulator = new(vertices.ToArray());
        int[] indices = triangulator.Triangulate();

        Mesh mesh = new()
        {
            vertices = vertices.Select(v => (Vector3)v).ToArray(),
            triangles = indices
        };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        filter.sharedMesh = mesh;
    }
}
