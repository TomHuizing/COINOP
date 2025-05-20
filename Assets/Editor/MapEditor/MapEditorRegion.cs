using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MapEditorRegion
{
    private static int idCounter = 0;
    public int Id { get; private set; }
    public List<MapEditorVertex> Vertices = new();

    public MapEditorRegion(List<MapEditorVertex> vertices)
    {
        this.Vertices.AddRange(vertices);
        this.Id = idCounter++;
    }

    public void Draw()
    {
        if (Vertices.Count < 2) return;

        Handles.color = new Color(0, 1, 0, 0.25f); // semi-transparent fill
        Vector3[] points = Vertices.ConvertAll(v => (Vector3)v.Position).ToArray();
        Handles.DrawAAConvexPolygon(points);

        Handles.color = Color.green;
        for (int i = 0; i < Vertices.Count; i++)
        {
            Vector3 from = Vertices[i].Position;
            Vector3 to = Vertices[(i + 1) % Vertices.Count].Position;
            Handles.DrawLine(from, to);
        }
    }
}
