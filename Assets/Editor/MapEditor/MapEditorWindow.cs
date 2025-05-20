using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class MapEditorWindow : EditorWindow
{
    private Texture2D mapTexture;
    private Vector2 scrollPos;

    private List<MapEditorVertex> vertices = new();
    private List<MapEditorRegion> regions = new();
    private Dictionary<MapEditorVertex, bool> vertexSelectionStates = new();

    //[MenuItem("Tools/Map Editor")]
    public static void OpenWindow()
    {
        GetWindow<MapEditorWindow>("Map Editor");
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        GUILayout.Label("Map Image", EditorStyles.boldLabel);
        mapTexture = (Texture2D)EditorGUILayout.ObjectField("Texture", mapTexture, typeof(Texture2D), false);

        if (GUILayout.Button("Clear All"))
        {
            if (EditorUtility.DisplayDialog("Confirm", "Are you sure you want to delete all vertices and regions?", "Yes", "No"))
            {
                vertices.Clear();
                regions.Clear();
                vertexSelectionStates.Clear();
                SceneView.RepaintAll();
            }
        }

        GUILayout.Space(10);
        GUILayout.Label("Vertices", EditorStyles.boldLabel);
        foreach (var vertex in vertices)
        {
            EditorGUILayout.BeginHorizontal();
            vertexSelectionStates[vertex] = EditorGUILayout.Toggle(vertexSelectionStates.ContainsKey(vertex) && vertexSelectionStates[vertex]);
            GUILayout.Label($"Vertex {vertex.Id} at {vertex.Position}");
            if (GUILayout.Button("Delete", GUILayout.Width(60)))
            {
                DeleteVertex(vertex);
                break;
            }
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Create Region from Selected Vertices"))
        {
            CreateRegionFromSelectedVertices();
        }

        GUILayout.Space(10);
        GUILayout.Label("Regions", EditorStyles.boldLabel);
        foreach (var region in regions.ToList())
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label($"Region {region.Id} ({region.Vertices.Count} vertices)");
            if (GUILayout.Button("Delete", GUILayout.Width(60)))
            {
                regions.Remove(region);
                SceneView.RepaintAll();
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        if (mapTexture != null)
        {
            Handles.BeginGUI();
            GUI.DrawTexture(new Rect(0, 0, mapTexture.width, mapTexture.height), mapTexture, ScaleMode.ScaleToFit);
            Handles.EndGUI();
        }

        DrawRegions();
        DrawVertices();

        HandleRightClick(e);
    }

    private void DrawVertices()
    {
        foreach (var vertex in vertices)
        {
            vertex.Draw(vertexSelectionStates.ContainsKey(vertex) && vertexSelectionStates[vertex]);
        }
    }

    private void DrawRegions()
    {
        foreach (var region in regions)
        {
            region.Draw();
        }
    }

    private void HandleRightClick(Event e)
    {
        if (e.type == EventType.ContextClick)
        {
            Vector2 mousePos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;

            GenericMenu menu = new();
            menu.AddItem(new GUIContent("Add Vertex"), false, () => AddVertex(mousePos));

            if (vertexSelectionStates.Any(v => v.Value))
                menu.AddItem(new GUIContent("Create Region"), false, CreateRegionFromSelectedVertices);
            else
                menu.AddDisabledItem(new GUIContent("Create Region"));

            if (vertices.Any())
                menu.AddItem(new GUIContent("Delete Selected Vertices"), false, DeleteSelectedVertices);
            else
                menu.AddDisabledItem(new GUIContent("Delete Selected Vertices"));

            menu.ShowAsContext();
            e.Use();
        }
    }

    private void AddVertex(Vector2 position)
    {
        var newVertex = new MapEditorVertex(position);
        vertices.Add(newVertex);
        vertexSelectionStates[newVertex] = false;
        SceneView.RepaintAll();
    }

    private void DeleteSelectedVertices()
    {
        foreach (var vertex in vertexSelectionStates.Where(kvp => kvp.Value).Select(kvp => kvp.Key).ToList())
        {
            DeleteVertex(vertex);
        }
    }

    private void DeleteVertex(MapEditorVertex vertex)
    {
        vertices.Remove(vertex);
        vertexSelectionStates.Remove(vertex);

        // Remove vertex from regions and delete invalid regions
        var affectedRegions = regions.Where(r => r.Vertices.Contains(vertex)).ToList();
        foreach (var region in affectedRegions)
        {
            region.Vertices.Remove(vertex);
            if (region.Vertices.Count < 3)
            {
                regions.Remove(region);
            }
        }

        SceneView.RepaintAll();
    }

    private void CreateRegionFromSelectedVertices()
    {
        var selectedVertices = vertexSelectionStates
            .Where(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToList();

        if (selectedVertices.Count >= 3)
        {
            var region = new MapEditorRegion(selectedVertices);
            regions.Add(region);

            foreach (var vertex in selectedVertices)
            {
                vertexSelectionStates[vertex] = false;
            }

            SceneView.RepaintAll();
        }
        else
        {
            EditorUtility.DisplayDialog("Insufficient Vertices", "You need at least 3 selected vertices to create a region.", "OK");
        }
    }
}
